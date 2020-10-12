import React, { createContext, useContext, useState } from "react";

const SimulacaoContext = createContext();

export default function SimulacaoProvider({ children }) {
  const [simulacao, setSimulacao] = useState([]);

  return (
    <SimulacaoContext.Provider
      value={{
        simulacao,
        setSimulacao,
      }}
    >
      {children}
    </SimulacaoContext.Provider>
  );
}

export function useSimulacao() {
  const context = useContext(SimulacaoContext);
  const { simulacao, setSimulacao } = context;
  return { simulacao, setSimulacao };
}
