import React from "react";

const InformacoesRecebimento = () => {
  return (
    <>
      <div style={{ margin: "50px 30px" }}>
        <h5>Informações de Recebimento</h5>
        <div style={{ marginTop: "30px" }}>
          <h6>Custo Vendedor</h6>
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
              <p>R$ --</p>
              <p>em -----</p>
            </div>
            <div>
              <p>e recebe</p>
              <p>R$ --</p>
              <p>em D+1</p>
            </div>
          </div>
          <hr style={{ margin: "0px" }} />
          <h6>Parcelado Cliente</h6>
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
              <p>R$ --</p>
              <p>em -----</p>
            </div>
            <div>
              <p>e recebe</p>
              <p>R$ --</p>
              <p>em D+1</p>
            </div>
          </div>
          <hr style={{ margin: "0px" }} />
          <h6>Custo Cliente</h6>
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
              <p>R$ --</p>
              <p>em -----</p>
            </div>
            <div>
              <p>e recebe</p>
              <p>R$ --</p>
              <p>em D+1</p>
            </div>
          </div>
          <hr />
        </div>
      </div>
    </>
  );
};

export default InformacoesRecebimento;
