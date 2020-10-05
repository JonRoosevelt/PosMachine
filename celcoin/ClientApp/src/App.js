import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'
import Maquineta from "./components/Maquineta";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route path="/maquineta" component={Maquineta} />
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
      </Layout>
    );
  }
}
