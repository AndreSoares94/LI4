import React, { Component } from 'react';
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import Routes from "./Routes";
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
library.add(fas)

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
          <Routes />
      );
  }
}

