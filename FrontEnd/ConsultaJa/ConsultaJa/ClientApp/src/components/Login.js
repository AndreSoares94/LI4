import React, { Component } from 'react';
import axios from 'axios';

import { CONTAS_URL } from './api';
import { ADMIN_URL } from './api';
import { Rodape } from './Rodape';
import { Redirect } from 'react-router-dom';
import { NavBarOut } from './NavBarOut.js';

export class Login extends Component {

    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            valido: false,
            type: '',
            senha: '',
            error: '',
            error1: '',
            dadosConta: [],
            dadosContaAdmin: []
        };
    }
    // Handler para o login como Paciente ou Medico
    handlerLogin = (event) => {
        event.preventDefault();

        axios.post(`${CONTAS_URL}/login`, {
            Email: this.state.email,
            Password: this.state.password
        })
            .then(response => {
                alert("Successfully logged in!!!");
                console.log(response);
                this.state.valido = true;
                this.setState({ dadosConta: response.data });
                localStorage.clear();
                localStorage.setItem("token", this.state.dadosConta.token);
                if (this.state.dadosConta.type[0] == 'P') {
                    this.state.type = "Paciente";
                    this.props.history.push("/perfilPaciente");
                } else if (this.state.dadosConta.type[0] == 'M') {
                    this.state.type = "Medico";
                    this.props.history.push("/perfilMedico");
                } else {
                    this.setState({
                        error:
                            "Houve um problema com o login, verifique as suas senhas."
                    });
                }
            })
            .catch(error => {
                this.setState({
                    error:
                        "Houve um problema com o login, verifique as suas senhas."
                });
                console.log(error);
            })
    }

    // Handler para o login como Administrador
    mySubmitHandler1 = (event) => {
        event.preventDefault();
        if (!this.state.senha) {
            this.setState({ error1: "Preencha as senhas para continuar!" });
        } else {
            axios.post(`${ADMIN_URL}/login`, {
                Senha: this.state.senha
            })
                .then(response => {
                    alert("Successfully logged in!!!");
                    console.log(response);
                    this.state.valido = true;
                    this.setState({ dadosContaAdmin: response.data });
                    localStorage.clear();
                    localStorage.setItem("token", this.state.dadosContaAdmin.token);
                    if (this.state.dadosContaAdmin.type[0] == 'A') {
                        this.state.type = "Admin";
                        this.props.history.push("/perfilAdmin");
                    } else {
                        this.setState({
                            error1:
                                "Houve um problema com o login, verifique as suas senhas."
                        });
                    }
                })
                .catch(error => {
                    this.setState({
                        error1:
                            "Houve um problema com o login, verifique as suas senhas."
                    });
                    console.log(error);
                })
        }
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    render() {
        if (this.state.valido) {
            if (this.state.type[0] === 'P') return (<Redirect to="/perfilPaciente" />);
            else if (this.state.type[0] === 'M') return (<Redirect to="/perfilMedico" />);
            else return (<Redirect to="/perfilAdmin" />);
        }
        return (
            <>
                <NavBarOut />
                <main>
                    <section className="relative block" style={{ height: "670px" }}>
                    <section className="absolute w-full h-full">
                        <div
                                className="absolute top-20 w-full h-full bg-blue-200"
                            style={{
                                backgroundImage:
                                    "url(" + require("./images/este.png") + ")",
                                backgroundSize: "40%",
                                backgroundRepeat: "no-repeat"
                            }}
                        ></div>
                        <div className="container mx-auto px-4 h-full">
                            <div className="flex content-center items-center justify-center h-full">
                                <div className="w-1/2 lg:w-5/12 px-6">
                                    <div className="relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded-lg bg-gray-300 border-0">
                                        
                                        <div className="flex-auto px-4 lg:px-10 py-10 pt-0">
                                            <div className="text-gray-600 text-center mb-3 font-bold">
                                                <small>Insira as suas credenciais</small>
                                            </div>

                                            <form onSubmit={this.handlerLogin}>
                                                <div className="relative w-full mb-3">
                                                    <label
                                                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                                                        htmlFor="grid-password"
                                                    >
                                                        Email
                            </label>
                                                    <input
                                                        className="form-control"
                                                        type="email"
                                                        name="email"
                                                        placeholder="Email"
                                                        style={{ transition: "all .15s ease" }}
                                                        onChange={this.myChangeHandler}
                                                    />
                                                </div>
                                                <div className="relative w-full mb-3">
                                                    <label
                                                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                                                        htmlFor="grid-password"
                                                    >
                                                        Password
                            </label>
                                                    <input
                                                        className="form-control"
                                                        type="password"
                                                        name="password"
                                                        placeholder="Password"
                                                        style={{ transition: "all .15s ease" }}
                                                        onChange={this.myChangeHandler}
                                                    />
                                                </div>


                                                <div className="text-center mt-6">
                                                    <button
                                                        className="bg-gray-900 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 w-full"
                                                        type="submit"
                                                        style={{ transition: "all .15s ease" }}
                                                    >
                                                        Login
                            </button>
                                                </div>

                                                {this.state.error && <p class="text-red-500 text-xs italic">{this.state.error}</p>}
                                            </form>
                                        </div>
                                    </div>
                                </div>

                                <div className="w-1/2 lg:w-5/12 px-4">
                                    <div className="relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded-lg bg-gray-300 border-0">

                                        <div className="flex-auto px-4 lg:px-10 py-10 pt-0">
                                            <div className="text-gray-600 text-center mb-3 font-bold">
                                                <small>Insira a sua password de Administrador</small>
                                            </div>

                                            <form onSubmit={this.mySubmitHandler1}>

                                                <div className="relative w-full mb-3">
                                                    <label
                                                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                                                        htmlFor="grid-password"
                                                    >
                                                        Password
                            </label>
                                                    <input
                                                        className="form-control"
                                                        type="password"
                                                        name="senha"
                                                        placeholder="Senha secreta"
                                                        style={{ transition: "all .15s ease" }}
                                                        onChange={this.myChangeHandler}
                                                    />
                                                </div>


                                                <div className="text-center mt-6">
                                                    <button
                                                        className="bg-gray-900 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 w-full"
                                                        type="submit"
                                                        style={{ transition: "all .15s ease" }}
                                                    >
                                                        Login como Administrador
                            </button>
                                                </div>

                                                {this.state.error1 && <p class="text-red-500 text-xs italic">{this.state.error1}</p>}
                                            </form>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        </section>
                        </section>
                </main>
                <Rodape />
            </>
        );
    }
}
