import React from "react";

import { Button } from "react-bootstrap";
import { useMeioPagamento } from "../context/MeioPagamento";

const MeioDePagamentoSelect = ({ meiosDePagamento }) => {
  const { meioPagamento, setMeioPagamento } = useMeioPagamento();

  const handleChange = (p) => {
    setMeioPagamento(p);
  };

  return (
    <>
      {meiosDePagamento.map((p) => (
        <Button
          style={{
            padding: "50px 100px",
            border: "2px solid grey",
            marginTop: "50px",
          }}
          className="d-flex justify-content-between"
          variant={p.nome == meioPagamento.nome ? "info" : "white"}
          aria-pressed="true"
          onClick={() => handleChange(p)}
          value={1}
          checked={meioPagamento}
        >
          {p.nome}
        </Button>
      ))}
    </>
  );
};

export default MeioDePagamentoSelect;
