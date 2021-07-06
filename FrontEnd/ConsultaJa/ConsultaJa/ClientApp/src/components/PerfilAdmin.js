import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

import api from './api';
import { NavMenuAdmin } from './NavMenuAdmin';
import { RodapeConta } from './RodapeConta';


export class PerfilAdmin extends Component {
    static displayName = PerfilAdmin.name;

    constructor(props) {
        super(props);
        const token = localStorage.getItem('token')

        let loggedIn = true
        if (token == null) {
            loggedIn = false
        }
        this.state = {
            idMed: [],
            loggedIn,
            novopreco: 0.00,
            dadosAdmin: [],
            medicosPendentes: []
        };
    }

    componentDidMount() {
        // Buscar os dados do preco
        api.get(`admin`, {
            params: {
                admin: 'admin'
            }
        })
            .then(res => { console.log(res); this.setState({ dadosAdmin: res.data }); })

            .catch(error => {
                alert("ERROR! " + error);
                console.log(error);
            });

        // Buscar a lista de medicos pendentes
        api.get(`/admin/listaMed`, {})
            .then(res => { console.log(res); this.setState({ medicosPendentes: res.data }); })
            .catch(error => {
                alert("ERROR! " + error);
                console.log(error);
            });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        api.get(`/admin/mudarpreco`, {
            params: {
                novopreco: this.state.novopreco
            }
        })
            .then(res => {
                console.log(res);
                alert("Preco mudado ");
                this.props.history.push("/");
            })
            .catch(err => {
                console.log(err);
            })
    }

    aceitar = (event) => {

        let val = event.target.dataset.medico;

        event.preventDefault();

        api.get(`/admin/aceitaMed`, {
            params: {
                id: val,
                action: 'true'
            }
        })
            .then(res => {
                console.log(res);
                alert("Novo Medico aceite ")
            })
            .catch(err => {
                console.log(err);
            })
    }

    rejeitar = (event) => {

        let val = event.target.dataset.medico;

        event.preventDefault();

        api.get(`/admin/aceitaMed`, {
            params: {
                id: val,
                action: 'false'
            }
        })
            .then(res => {
                console.log(res);
                alert("Novo Medico rejeitado ")
            })
            .catch(err => {
                console.log(err);
            })
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }


    render() {
        if (this.state.loggedIn === false) {
            return (<Redirect to="/login" />);
        }
        return (
            <>
                <NavMenuAdmin />
                <main className="profile-page">
                    <section className="relative block" style={{ height: "400px" }}>
                        <div
                            className="absolute top-0 w-full h-full bg-center bg-cover"
                            style={{
                                backgroundImage:
                                    "url('https://images.unsplash.com/photo-1550751827-4bd374c3f58b?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80')"
                            }}
                        >
                            <span
                                id="blackOverlay"
                                className="w-full h-full absolute opacity-50 bg-black"
                            ></span>
                        </div>
                        <div
                            className="top-auto bottom-0 left-0 right-0 w-full absolute pointer-events-none overflow-hidden"
                            style={{ height: "70px", transform: "translateZ(0)" }}
                        >
                            <svg
                                className="absolute bottom-0 overflow-hidden"
                                preserveAspectRatio="none"
                                version="1.1"
                                viewBox="0 0 2560 100"
                                x="0"
                                y="0"
                            >
                                <polygon
                                    className="text-gray-300 fill-current"
                                    points="2560 0 2560 100 0 100"
                                ></polygon>
                            </svg>
                        </div>
                    </section>
                    <section className="relative py-16 bg-gray-300">
                        <div className="container mx-auto px-4">
                            <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-xl rounded-lg -mt-64">
                                <div className="px-6">

                                    <div className="text-center mt-8">

                                        <h3 className="text-4xl font-semibold leading-normal mb-2 text-gray-800 mb-2">
                                            Preço por Consulta: {this.state.dadosAdmin.preco} €
                                        </h3>

                                        <form class="mb-4" onSubmit={this.handleSubmit}>
                                            <input class="appearance-none bg-gray-200 text-gray-700 border border-gray-200 rounded mr-8 py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                type='number'
                                                min='0.0'
                                                step='0.50'
                                                name='novopreco'
                                                placeholder="Novo preco"
                                                onChange={this.myChangeHandler}
                                            />

                                            <input class="hover:bg-indigo-700 bg-blue-500 text-white p-2 rounded font-bold overflow-visible" type='submit' value="Alterar Preco Consulta" />
                                        </form>

                                        <div className="mb-2 text-gray-700 mt-2">
                                            <FontAwesomeIcon icon="user-md" /> Número de Médicos registados: <span class="badge mb-3 bg-blue-500 rounded-full px-3 py-2 text-center object-right-top text-white text-sm mr-12"> {this.state.dadosAdmin.numMedicos}</span>
                                        </div>

                                        <div className="text-gray-700 mt-2">
                                            <FontAwesomeIcon icon="user" /> Número de Pacientes registados: <span class="badge mb-3 bg-blue-500 rounded-full px-3 py-2 text-center object-right-top text-white text-sm mr-12">{this.state.dadosAdmin.numPacientes}</span>
                                        </div>
                                    </div>

                                    <div className="mt-4 py-4 border-t border-gray-300 text-center">
                                        <div className="flex flex-wrap justify-center">
                                            <div className="w-full lg:w-9/12 px-4">
                                                <p className="mb-4 text-lg leading-relaxed text-gray-800">
                                                    Pedidos de inscrição de Médicos: {this.state.medicosPendentes.length}
                                                </p>
                                                <div>
                                                    <table class="border-collapse w-full">
                                                        <thead>
                                                            <tr>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Nome</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Email</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Contacto</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Data de Nascimento</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-700 border-top border-gray-300 hidden lg:table-cell"></th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-700 border-top border-right border-gray-300 hidden lg:table-cell"></th>
                                                            </tr>
                                                        </thead>
                                                        {this.state.medicosPendentes.map(medico =>
                                                            <tr>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{medico.nome}</td>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{medico.email}</td>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{medico.contactos}</td>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{medico.dataNascimento}</td>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">
                                                                    <button class="hover:bg-blue-500 bg-blue-400 text-blue-dark font-semibold text-white py-2 px-3 border rounded" key={medico.id} data-medico={medico.id} onClick={this.aceitar}> Aceitar </button> </td>
                                                                <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">
                                                                    <button class="hover:bg-red-500 bg-red-400 text-blue-dark font-semibold text-white py-2 px-3 border rounded" key={medico.id} data-medico={medico.id} onClick={this.rejeitar}> Rejeitar </button> </td>
                                                            </tr>)
                                                        }
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </section>
                </main>
                <RodapeConta />
            </>
        )
    }
}

    /*
    render() {
        return (
            <LayoutAdmin >
                <h1> Perfil Administrador </h1>
                <div class="op3Admin">
                    <form onSubmit={this.handleSubmit}>
                        <input
                            type='number'
                            min='0.0'
                            step='0.01'
                            name='novopreco'
                            placeholder="Novo preco"
                            onChange={this.myChangeHandler}
                        />
                        <br />
                        <input class="Regs" type='submit' value="Alterar Preco Consulta" />
                    </form>
                    <div />
                    <div className="perfilAd">
                        <h5> Preço por Consulta: {this.state.dadosAdmin.preco} € </h5>
                        <h5> Número de Médicos: {this.state.dadosAdmin.numMedicos} </h5>
                        <h5> Número de Pacientes: {this.state.dadosAdmin.numPacientes} </h5>
                    </div>
                </div>
                <div class="op4Admin">
                    <h1 className="title"> Pedidos de inscrição de Médicos </h1>
                    <div>

                    </div>
                    <table>
                        <tr>
                            <th>Nome</th>
                            <th>Email</th>
                            <th>Data de Nascimento</th>
                        </tr>
                        {this.state.medicosPendentes.map(medico =>
                            <tr>
                                <td>{medico.nome}</td>
                                <td>{medico.email}</td>
                                <td>{medico.dataNascimento}</td>
                                <td> <button key={medico.id} data-medico={medico.id} onClick={this.aceitar}> Aceitar </button> </td>
                                <td> <button key={medico.id} data-medico={medico.id} onClick={this.rejeitar}> Rejeitar </button> </td>
                            </tr>)
                        }
                    </table>
                    <div>
                        <h3> Número de Pedidos de Médico: {this.state.medicosPendentes.length}</h3>
                    </div>
                </div>
            </LayoutAdmin >
        )
    }
}*/