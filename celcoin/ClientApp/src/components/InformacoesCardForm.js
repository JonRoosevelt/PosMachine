import React, { useState, useEffect } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import { useMeioPagamento } from "../context/MeioPagamento";
import Axios from "axios";

const InformacoesCardForm = ({ taxas }) => {
  const { meioPagamento } = useMeioPagamento();
  const [meioPagamentoId, setMeioPagamentoId] = useState();
  const [taxaParcelaId, setTaxaParcelaId] = useState();
  const [taxaParcela, setTaxaParcela] = useState();
  const [listaDeTaxas, setListaDeTaxas] = useState(taxas);
  const numParcelas = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
      event.preventDefault();
      event.stopPropagation();
    }
    const {
      formParcelas,
      formTaxaIntermediacao,
      formVenda,
      formTaxaParcelamento,
    } = form.elements;
    const venda = {
      meioPagamentoId,
      numParcelas: formParcelas.value,
      taxaIntermediacao: formTaxaIntermediacao.value,
      valorVenda: formVenda.value,
      taxaParcelaId,
      valorTaxa: formTaxaParcelamento.value,
    };
    alert(venda);
  };

  const setTaxaParcelaIdAndValue = (meioPagamento) => {
    let taxaId = 0;
    if (meioPagamento.nome === "Débito") {
      taxaId = 1003;
    } else {
      taxaId = 1002;
    }
    setTaxaParcelaId(taxaId);
    if (listaDeTaxas > 0) {
      setTaxaParcela(listaDeTaxas.filter((taxa) => taxa.id == taxaId));
    }
  };

  useEffect(() => {
    setMeioPagamentoId(meioPagamento.id);
    setTaxaParcelaIdAndValue(meioPagamento);
  }, [meioPagamento]);

  useEffect(() => {
    setListaDeTaxas(taxas);
  }, [taxas]);

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
                value={taxaParcela}
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
