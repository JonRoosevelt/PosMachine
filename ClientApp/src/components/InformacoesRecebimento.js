import React from "react";
import { useSimulacao } from "../context/Simulacao";

const InformacoesRecebimento = () => {
  const { simulacao } = useSimulacao();
  return (
    <div
      className="container"
      style={{ marginTop: "50px", marginBottom: "50px" }}
    >
      <div className="row justify-content-center">
        <h5>Informações de Recebimento</h5>
        <div style={{ marginTop: "10px" }}>
          {simulacao.map((t) => (
            <>
              <h6>{t.tipoVenda.nome.replace("_", " ")}</h6>
              <div
                className="d-flex justify-content-start"
                style={{
                  display: "grid",
                  gridTemplateColumns: "1fr 1fr",
                  lineHeight: "75%",
                }}
              >
                <div style={{ marginRight: "100px" }}>
                  <p>você cobra</p>
                  <p>R$ {t.valorVenda.toFixed(2)}</p>
                  <p>
                    em {t.numParcelas}x de R$
                    {(t.recebivel / t.numParcelas).toFixed(2)}
                  </p>
                </div>
                <div>
                  <p>e recebe</p>
                  <p>R$ {t.recebivel.toFixed(2)}</p>
                  <p>em D+1</p>
                </div>
              </div>
            </>
          ))}
        </div>
      </div>
    </div>
  );
};

export default InformacoesRecebimento;
