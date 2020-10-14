import React, { useState, useEffect } from "react";
import { Formik, Field, Form } from "formik";
import { useMeioPagamento } from "../context/MeioPagamento";
import { useSimulacao } from "../context/Simulacao";
import axios from "axios";
import { API_HOST } from "../consts";

const InformacoesCardForm = ({ taxas }) => {
  const { meioPagamento } = useMeioPagamento();
  const { setSimulacao } = useSimulacao();

  const [taxaParcela, setTaxaParcela] = useState({
    id: 1003,
    nome: "taxa_parcela_debito",
    valor: 0.0,
  });

  const numParcelas = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  const fetchSimulacao = async (venda) => {
    console.log(venda);
    await axios
      .post(`${API_HOST}/v1/simular`, venda)
      .then((response) => setSimulacao(response.data))
      .catch((err) => alert(err));
  };

  const createTaxa = async (taxa) => {
    const data = axios
      .post(`${API_HOST}/v1/taxas`, taxa)
      .then((res) => res.data);
    return data;
  };

  const getNomeMeioPagamento = () => {
    let nomeMeioPagamento = "debito";
    if (meioPagamento.nome === "Crédito") {
      nomeMeioPagamento = "credito";
    }
    return nomeMeioPagamento;
  };

  const getOrCreateTaxaParcela = async (values) => {
    let { valorTaxa } = values;
    valorTaxa = parseFloat(valorTaxa);
    let taxa = taxas.find((taxa) => taxa.valor === valorTaxa);
    if (taxa) {
      console.log(taxa.id);
      return taxa.id;
    }
    const nomeMeioPagamento = getNomeMeioPagamento();
    taxa = await createTaxa({
      nome: `taxa-parcelamento-${nomeMeioPagamento}-${valorTaxa}`,
      valor: valorTaxa.toFixed(2),
    });
    return taxa.id;
  };

  const getOrCreateTaxaIntermediacao = async (values) => {
    let { taxaIntermediacao } = values;
    taxaIntermediacao = parseFloat(taxaIntermediacao);
    let taxa = taxas.find((taxa) => taxa.valor === taxaIntermediacao);
    if (taxa) {
      return taxa.id;
    }
    const nomeMeioPagamento = getNomeMeioPagamento();
    taxa = await createTaxa({
      nome: `taxa-intermediacao-${nomeMeioPagamento}-${taxaIntermediacao}`,
      valor: parseFloat(taxaIntermediacao).toFixed(2),
    });
    return taxa.id;
  };

  const handleSubmit = async (values) => {
    const taxaIntermediacao = await getOrCreateTaxaIntermediacao(values);
    const taxaParcelaId = await getOrCreateTaxaParcela(values);

    const { valorVenda, numParcelas, taxaParcelamento } = values;
    const { id } = meioPagamento;
    const venda = {
      taxaIntermediacao,
      valorTaxa: taxaParcelamento,
      valorVenda,
      numParcelas: parseInt(numParcelas),
      vendedorId: 1000,
      meioPagamentoId: id,
      tipoVendaId: 1000,
      taxaIntermediacao,
      taxaParcelaId,
    };
    fetchSimulacao(venda);
  };

  const updateTaxaParcelaId = (meioPagamentoId) => {
    let taxaId = 1003;
    if (meioPagamentoId === 1001) {
      taxaId = 1002;
    }
    const taxa = taxas.find((taxa) => taxa.id === taxaId);
    setTaxaParcela(taxa);
  };

  useEffect(() => {
    updateTaxaParcelaId(meioPagamento.id);
  }, [meioPagamento, taxaParcela]);

  return (
    <div
      className="container"
      style={{ marginTop: "50px", marginBottom: "50px" }}
    >
      <div className="row justify-content-center">
        <h5>Informações de Cobrança</h5>
        <div className="col-lg-8">
          <Formik
            initialValues={{
              taxaIntermediacao: meioPagamento.taxa.valor,
              valorTaxa: 0.0,
              numParcelas: 1,
            }}
            onSubmit={(values) => handleSubmit(values)}
          >
            <Form>
              <div
                className="row justify-content-md-center"
                style={{ marginTop: "10px" }}
              >
                <div className="col">
                  <label>Taxa de Intermediação</label>
                  <Field
                    type="number"
                    name="taxaIntermediacao"
                    placeholder="0,00%"
                    className="form-control"
                    value={meioPagamento.taxa.valor}
                  />
                </div>
                <div className="col">
                  <label>Taxa de Parcelamento</label>
                  <Field
                    type="number"
                    name="taxaParcelamento"
                    placeholder="0,00%"
                    className="form-control"
                    value={taxaParcela?.valor ? taxaParcela.valor : 0.0}
                  />
                </div>
              </div>
              <div className="form-group">
                <label>Valor da Venda</label>
                <Field
                  type="number"
                  name="valorVenda"
                  placeholder="R$ 0,00"
                  className="form-control"
                />
              </div>
              <div className="form-group">
                <label>Parcelas</label>
                <Field
                  as="select"
                  name="numParcelas"
                  className="form-control"
                  defaultChecked={1}
                  disabled={meioPagamento.nome === "Débito"}
                >
                  {numParcelas.map((num) => (
                    <option key={num}>{num}</option>
                  ))}
                </Field>
              </div>
              <button
                type="submit"
                className="btn btn-secondary btn-block"
                style={{ marginTop: "30px" }}
              >
                Simular
              </button>
            </Form>
          </Formik>
        </div>
      </div>
    </div>
  );
};

export default InformacoesCardForm;
