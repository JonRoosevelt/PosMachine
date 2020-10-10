import React, { useState, useEffect } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import { useMeioPagamento } from "../context/MeioPagamento";
import { useSimulacao } from "../context/Simulacao";
import axios from "axios";
import { API_HOST } from '../consts';

const InformacoesCardForm = ({ taxas }) => {
  const { meioPagamento } = useMeioPagamento();
  const { simulacao, setSimulacao } = useSimulacao();
  const [meioPagamentoId, setMeioPagamentoId] = useState(1000);

  const [taxaParcela, setTaxaParcela] = useState({
    id: 1003,
    nome: "taxa_parcela_debito",
    valor: 0.0,
  });

  const numParcelas = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  const fetchSimulacao = async (venda) => {
    await axios
      .post(`${API_HOST}/v1/simular`, { data: venda })
      .then((response) => setSimulacao(response.data))
      .catch(err => alert(err))
  };


  const updateSimulacao = (formInfo) => {
    const {
      formParcelas,
      formTaxaIntermediacao,
      formVenda,
      formTaxaParcelamento,
    } = formInfo;

    const venda = ({
      vendedorId: 1000,
      meioPagamentoId: meioPagamentoId || 1000,
      tipoVendaId: 1000,
      numParcelas: formParcelas.value,
      taxaIntermediacao: formTaxaIntermediacao.value,
      valorVenda: formVenda.value,
      taxaParcela: taxaParcela.id,
      valorTaxa: formTaxaParcelamento.value,
    });

    fetchSimulacao(venda)

    console.log(simulacao)
  };

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
      event.preventDefault();
      event.stopPropagation();
    }

    const formInfo = form.elements;

    updateSimulacao(formInfo);
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
    setMeioPagamentoId(meioPagamento.id);
    updateTaxaParcelaId(meioPagamento.id);
  }, [meioPagamento]);

  return (
    <div style={{ margin: "50px 30px" }}>
      <h5>Informações de Cobrança</h5>
      <div style={{ marginTop: "30px" }}>
        <Form onSubmit={handleSubmit}>
          <Form.Row>
            <Form.Group as={Col} controlId="formTaxaIntermediacao">
              <Form.Label>Taxa de Intermediação</Form.Label>
              <Form.Control
                type="number"
                placeholder="0,00%"
                value={meioPagamento.taxa.valor}
              />
            </Form.Group>
            <Form.Group as={Col} controlId="formTaxaParcelamento">
              <Form.Label>Taxa de Parcelamento</Form.Label>
              <Form.Control
                type="number"
                placeholder="0,00%"
                value={taxaParcela ? taxaParcela.valor : 0.0}
              />
            </Form.Group>
          </Form.Row>
          <Form.Row>
            <Form.Group as={Col} controlId="formVenda">
              <Form.Label>Valor da Venda</Form.Label>
              <Form.Control type="number" placeholder="R$ 0,00" />
            </Form.Group>
          </Form.Row>
          <Form.Row>
            <Form.Group controlId="formParcelas">
              <Form.Control as="select" style={{ width: "500px" }}>
                {numParcelas.map((num) => (
                  <option>{num}</option>
                ))}
              </Form.Control>
            </Form.Group>
          </Form.Row>
          <Button
            variant="secondary"
            style={{ width: "500px", marginTop: "50px" }}
            type="submit"
          >
            SIMULAR
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default InformacoesCardForm;
