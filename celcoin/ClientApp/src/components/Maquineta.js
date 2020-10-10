import axios from "axios";
import React, { useState, useEffect } from "react";
import { API_HOST } from "../consts";
import MeioPagamentoProvider from "../context/MeioPagamento";
import SimulacaoProvider from "../context/Simulacao";
import InformacoesCardForm from "./InformacoesCardForm";
import InformacoesRecebimento from "./InformacoesRecebimento";
import MeioDePagamentoSelect from "./MeioDePagamentoSelect";


const Maquineta = () => {
  const [meiosPagamento, setMeiosPagamento] = useState([]);
  const [taxas, setTaxas] = useState([]);

  const fetchMeiosPagamento = async () => {
    await axios
      .get(`${API_HOST}/v1/meios-de-pagamento`)
      .then((response) => setMeiosPagamento(response.data));
  };

  const fetchTaxas = async () => {
    await axios
      .get(`${API_HOST}/v1/taxas`)
      .then((response) => setTaxas(response.data));
  };

  useEffect(() => {
    fetchMeiosPagamento();
    fetchTaxas();
  }, []);
  return (
    <MeioPagamentoProvider>
      <h1 className="d-flex justify-content-center">Pagamento</h1>
      <span className="d-flex justify-content-center">
        Escolha o método de pagamento abaixo.
      </span>
      <div
        className="d-flex justify-content-around"
        style={{
          display: "grid",
          gridTemplateColumns: "1fr 1fr",
        }}
      >
        <MeioDePagamentoSelect meiosDePagamento={meiosPagamento} />
      </div>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "1fr 1fr",
        }}
      >
        <SimulacaoProvider>
          <InformacoesCardForm taxas={taxas} />
          <InformacoesRecebimento />
        </SimulacaoProvider>
      </div>
    </MeioPagamentoProvider>
  );
};

export default Maquineta;
