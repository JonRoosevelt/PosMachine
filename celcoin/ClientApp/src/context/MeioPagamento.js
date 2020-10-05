import React, { createContext, useContext, useState } from "react";

const MeioPagamentoContext = createContext();

export default function MeioPagamentoProvider({ children }) {
  const [meioPagamento, setMeioPagamento] = useState({
    id: 1000,
    nome: "Débito",
    taxaId: 1000,
    taxa: {
      id: 1000,
      nome: "taxa_debito",
      valor: 2.3,
    },
  });

  return (
    <MeioPagamentoContext.Provider
      value={{
        meioPagamento,
        setMeioPagamento,
      }}
    >
      {children}
    </MeioPagamentoContext.Provider>
  );
}

export function useMeioPagamento() {
  const context = useContext(MeioPagamentoContext);
  const { meioPagamento, setMeioPagamento } = context;
  return { meioPagamento, setMeioPagamento };
}
