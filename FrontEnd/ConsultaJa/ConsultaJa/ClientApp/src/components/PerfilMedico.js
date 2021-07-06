import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import decode from 'jwt-decode';
import logo from './images/logo_consultaJa.png';

import ImgPerfil from './images/profile-placeholder.jpg';
import api from './api';
import { NavMenuMedico } from './NavMenuMedico';
import { RodapeConta } from './RodapeConta';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'


export class PerfilMedico extends Component {
    static displayName = PerfilMedico.name;

    constructor(props) {
        super(props);
        const token = localStorage.getItem("token")

        let loggedIn = true
        if (token == null) {
            loggedIn = false
        }
        this.state = {
            id: '',
            loggedIn,
            firstName: '',
            dadosPerfil: [],
            consultasAgendadas: [],
            notificacoes: [],
            name: null,
            dataNascimento: null,
            morada: null,
            codigo_postal: null,
            passwordAntiga: null,
            passwordNova: null,
            nif: null,
            contactos: null,
            localidade: null
        };
    }

    componentDidMount() {
        const token = localStorage.getItem('token');
        var decoded = decode(token);
        const idD = decoded.Id;
        //console.log("Id" + idD);
        this.state.id = idD;
        if (localStorage.getItem("nome") != null) {
            this.setState({ firstName: localStorage.getItem("nome") });
        }
        if (localStorage.getItem('intervalo') == null) {
            var notifications = localStorage.getItem('notify')
            if (notifications == null) {
                notifications = []
            }
            api.get(`consultas/notify`, {
                params: {
                    id: idD
                }
            })
                .then(res => {
                    if (!notifications.includes(res.data)) {
                        notifications.push(res.data)
                        localStorage.setItem('notify', notifications)
                        alert("[NOVA NOTIFICAÇÃO]\n" + res.data);
                    }
                })
                .catch(error => {
                    console.log(error);
                });
            const idIntervalo = setInterval(() => {
                var notifications = localStorage.getItem('notify')
                if (notifications == null) {
                    notifications = []
                }
                const token = localStorage.getItem('token');
                var decoded = decode(token);
                const idD = decoded.Id;
                api.get(`consultas/notify`, {
                    params: {
                        id: idD
                    }
                })
                    .then(res => {
                        if (!notifications.includes(res.data)) {
                            notifications.push(res.data)
                            localStorage.setItem('notify', notifications)
                            alert("[NOVA NOTIFICAÇÃO]\n" + res.data);
                        }
                    })
                    .catch(error => {
                        //alert(error.data);
                        console.log(error);
                    });
            }, 60000);
            localStorage.setItem('intervalo', idIntervalo);
        }
        // Buscar os dados do medico
        api.get(`/contas/${this.state.id}`)
            .then(res => {
                console.log(res);
                this.setState({ dadosPerfil: res.data });
                if (localStorage.getItem("nome") == null) {
                    this.setState({ firstName: res.data.nome });
                    localStorage.setItem("nome", this.state.firstName);
                }
            })
            .catch(error => {
                alert("ERROR! " + error);
                console.log(error);
            });
        // Buscar a lista de consultas agendadas
        api.get(`/consultas/listaAg`, {
            params: {
                id: this.state.id
            }
        })
            .then(res => { console.log(res); this.setState({ consultasAgendadas: res.data }); })
            .catch(error => {
                alert("ERROR! " + error);
                console.log(error);
            });
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    render() {
        return (
            <>
                <nav className="md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden shadow-xl bg-blue-200 bg-opacity-75 flex flex-wrap items-center justify-between relative md:w-64 z-10 py-4 px-6">

                    {/* Bem-Vindo */}
                    <div className="w-full relative flex justify-between lg:w-auto lg:static lg:block lg:justify-start">
                        <Link tag={Link} className="links" to="/perfilMedico">
                            <a
                                className="text-white text-sm font-bold leading-relaxed inline-block mr-4 py-2 whitespace-no-wrap uppercase"
                            >
                                <img className="text-gray-800 text-xs font-bold uppercase px-1 py-1 hover:shadow-md outline-none focus:outline-none lg:mr-1 lg:mb-0 ml-3 mb-3"
                                    width={180} src={logo} />
                            </a>
                        </Link>
                    </div>

                    <div
                        className="md:block text-left text-xl md:pb-2 text-gray-800 mr-0 inline-block whitespace-no-wrap text-sm uppercase font-bold p-3 px-0"
                    >
                        Bem Vindo(a) <br /> {this.state.firstName.split(' ', 1)}
                    </div>

                    <ul className="md:flex-col md:min-w-full flex flex-col list-none">
                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/perfilMedico"
                            >
                                <FontAwesomeIcon icon="user" /><i className="text-gray-500 mr-2 text-sm"></i> Perfil
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/propostasConsultaM"
                            >
                                <FontAwesomeIcon icon="syringe" /> <i className="opacity-75 mr-2 text-sm"></i> Propostas de Consulta
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/posconsulta"
                            >
                                <FontAwesomeIcon icon="paste" /><i className="text-gray-500 mr-2 text-sm"></i> Pos Consulta
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/historicoMedico"
                            >
                                <FontAwesomeIcon icon="clipboard-list" /><i className="text-gray-500 mr-2 text-sm"></i> Histórico de Consultas
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/editarPerfilM"
                            >
                                <FontAwesomeIcon icon="edit" /><i className="text-gray-500 mr-2 text-sm"></i> Editar Perfil
                                    </Link>
                        </li>
                    </ul>
                    {/* Divider */}
                    <hr className="my-4 md:min-w-full" />
                </nav>

                <main className="relative md:ml-64 profile-page">
                    <NavMenuMedico />
                    <section className="relative block" style={{ height: "400px" }}>
                        <div
                            className="absolute top-0 w-full h-full bg-center bg-cover"
                            style={{
                                backgroundImage:
                                    "url('https://images.unsplash.com/photo-1532938911079-1b06ac7ceec7?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1189&q=80')"
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
                                    <div className="flex flex-wrap justify-center">
                                        <div className="w-full lg:w-3/12 px-4 lg:order-2 flex justify-center pb-32">
                                            <div className="relative">
                                                <img
                                                    alt="..."
                                                    src={require("./images/profile-placeholder.jpg")}
                                                    className="shadow-xl rounded-full h-auto align-middle border-none absolute -m-16 -ml-20 lg:-ml-16"
                                                    style={{ maxWidth: "150px" }}
                                                />
                                            </div>
                                        </div>

                                    </div>
                                    <div className="text-center mt-0">
                                        <h3 className="text-4xl font-semibold leading-normal mb-2 text-gray-800 mb-2">
                                            {this.state.dadosPerfil.nome}
                                        </h3>
                                        <div className="text-sm leading-normal mt-0 mb-2 text-gray-500 font-bold uppercase">
                                            <i className="fas fa-map-marker-alt mr-2 text-lg text-gray-500"></i>{" "}
                                            {this.state.dadosPerfil.email}
                                        </div>
                                        <div className="text-sm leading-normal mt-0 mb-2 text-gray-500 font-bold uppercase">
                                            <i className="fas fa-map-marker-alt mr-2 text-lg text-gray-500"></i>
                                            {this.state.dadosPerfil.dataNascimento}
                                        </div>
                                    </div>

                                    <div className="mt-10 py-10 border-t border-gray-300 text-center">
                                        <div className="flex flex-wrap justify-center">
                                            <div className="w-full lg:w-9/12 px-4">
                                                <p className="mb-4 text-xl text-bold leading-relaxed text-gray-800">
                                                    Consultas Agendadas: {this.state.consultasAgendadas.length}
                                                </p>
                                                <div>
                                                    <table class="border-collapse w-full">
                                                        <thead>
                                                            <tr>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Data</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Hora</th>
                                                                <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Paciente</th>
                                                            </tr>
                                                        </thead>
                                                        {this.state.consultasAgendadas.map(consulta => <tr>
                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{consulta.data}</td>
                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{consulta.hora}</td>
                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">Sr(a). {consulta.paciente}</td>
                                                        </tr>)}
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    <RodapeConta />
                </main>
            </>
        )
    }



    //render() {
    //    return (
    //        <>
    //            <NavMenuMedico />
    //            <main className="profile-page">
    //                <section className="relative block" style={{ height: "400px" }}>
    //                    <div
    //                        className="absolute top-0 w-full h-full bg-center bg-cover"
    //                        style={{
    //                            backgroundImage:
    //                                "url('https://images.unsplash.com/photo-1532938911079-1b06ac7ceec7?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1189&q=80')"
    //                        }}
    //                    >
    //                        <span
    //                            id="blackOverlay"
    //                            className="w-full h-full absolute opacity-50 bg-black"
    //                        ></span>
    //                    </div>
    //                    <div
    //                        className="top-auto bottom-0 left-0 right-0 w-full absolute pointer-events-none overflow-hidden"
    //                        style={{ height: "70px", transform: "translateZ(0)" }}
    //                    >
    //                        <svg
    //                            className="absolute bottom-0 overflow-hidden"
    //                            preserveAspectRatio="none"
    //                            version="1.1"
    //                            viewBox="0 0 2560 100"
    //                            x="0"
    //                            y="0"
    //                        >
    //                            <polygon
    //                                className="text-gray-300 fill-current"
    //                                points="2560 0 2560 100 0 100"
    //                            ></polygon>
    //                        </svg>
    //                    </div>
    //                </section>
    //                <section className="relative py-16 bg-gray-300">
    //                    <div className="container mx-auto px-4">
    //                        <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-xl rounded-lg -mt-64">
    //                            <div className="px-6">
    //                                <div className="flex flex-wrap justify-center">
    //                                    <div className="w-full lg:w-3/12 px-4 lg:order-2 flex justify-center">
    //                                        <div className="relative">
    //                                            <img
    //                                                alt="..."
    //                                                src={require("./images/profile-placeholder.jpg")}
    //                                                className="shadow-xl rounded-full h-auto align-middle border-none absolute -m-16 -ml-20 lg:-ml-16"
    //                                                style={{ maxWidth: "150px" }}
    //                                            />
    //                                        </div>
    //                                    </div>
    //                                    <div className="w-full lg:w-4/12 px-4 lg:order-3 lg:text-right lg:self-center">
    //                                        <div className="py-6 px-3 mt-32 sm:mt-0">
    //                                            <Link tag={Link} className="links" to="/historicoMedico">
    //                                                <button
    //                                                    className="bg-blue-500 uppercase text-white font-bold hover:shadow-md shadow text-xs px-4 py-2 rounded outline-none focus:outline-none mb-1"
    //                                                    type="button"
    //                                                >
    //                                                    Histórico de Consultas
    //                                            </button>
    //                                            </Link>
    //                                        </div>
    //                                        <div className="py-6 px-3 mt-32 sm:mt-0">
    //                                            <Link tag={Link} className="links" to="/posconsulta">
    //                                                <button
    //                                                    className="bg-blue-500 uppercase text-white font-bold hover:shadow-md shadow text-xs px-4 py-2 rounded outline-none focus:outline-none mb-1"
    //                                                    type="button"
    //                                                >
    //                                                    Pós Consulta
    //                                            </button>
    //                                            </Link>
    //                                        </div>
    //                                    </div>
    //                                    <div className="w-full lg:w-4/12 px-4 lg:order-1">
    //                                        <div className="py-6 px-3 mt-32 sm:mt-0">
    //                                            <Link tag={Link} className="links" to="/propostasConsultaM">
    //                                                <button
    //                                                    className="bg-blue-500 uppercase text-white font-bold hover:shadow-md shadow text-xs px-4 py-2 rounded outline-none focus:outline-none mb-1"
    //                                                    type="button"
    //                                                >
    //                                                    Propostas de Consulta
    //                                            </button>
    //                                            </Link>
    //                                        </div>
    //                                        <div className="py-6 px-3 mt-32 sm:mt-0">
    //                                            <button
    //                                                className="bg-blue-500 uppercase text-white font-bold hover:shadow-md shadow text-xs px-4 py-2 rounded outline-none focus:outline-none mb-1"
    //                                                type="button"
    //                                                onClick={this.handleOnAccept}
    //                                            >
    //                                                Editar Perfil
    //                                            </button>
    //                                        </div>

    //                                        <div>{(!this.state.isToggleOn) ? ""
    //                                            : <div>
    //                                                <form onSubmit={this.handleEdit}>
    //                                                    <div>
    //                                                        <input
    //                                                            type="text"
    //                                                            name='name'
    //                                                            placeholder="Nome"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <div>
    //                                                        <input
    //                                                            type='date'
    //                                                            name='dataNascimento'
    //                                                            placeholder="Data de Nascimento"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <div>
    //                                                        <input
    //                                                            type="password"
    //                                                            name='passwordAntiga'
    //                                                            placeholder="Password Atual"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <div>
    //                                                        <input
    //                                                            type="password"
    //                                                            name='passwordNova'
    //                                                            placeholder="Nova Password"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <div>
    //                                                        <input
    //                                                            type="text"
    //                                                            name='morada'
    //                                                            placeholder="Morada"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <div>
    //                                                        <input
    //                                                            type="text"
    //                                                            name='codigo_postal'
    //                                                            placeholder="XXXX-XXX"
    //                                                            onChange={this.myChangeHandler}
    //                                                        /> </div>
    //                                                    <br />
    //                                                    <br />
    //                                                    <input type='submit' value="Editar" />
    //                                                </form></div>}
    //                                        </div>

    //                                    </div>
    //                                </div>
    //                                <div className="text-center mt-0">
    //                                    <h3 className="text-4xl font-semibold leading-normal mb-2 text-gray-800 mb-2">
    //                                        {this.state.dadosPerfil.nome}
    //                                    </h3>
    //                                    <div className="text-sm leading-normal mt-0 mb-2 text-gray-500 font-bold uppercase">
    //                                        <i className="fas fa-map-marker-alt mr-2 text-xl text-gray-500"></i>{" "}
    //                                        {this.state.dadosPerfil.email}
    //                                    </div>
    //                                    <div className="text-sm leading-normal mt-0 mb-2 text-gray-500 font-bold uppercase">
    //                                        <i className="fas fa-map-marker-alt mr-2 text-xl text-gray-500"></i>
    //                                        {this.state.dadosPerfil.dataNascimento}
    //                                    </div>
    //                                </div>

    //                                <div className="mt-10 py-10 border-t border-gray-300 text-center">
    //                                    <div className="flex flex-wrap justify-center">
    //                                        <div className="w-full lg:w-9/12 px-4">
    //                                            <p className="mb-4 text-2xl text-bold leading-relaxed text-gray-800">
    //                                                Consultas Agendadas: {this.state.consultasAgendadas.length}
    //                                            </p>
    //                                            <div>
    //                                                <table class="border-collapse w-full">
    //                                                    <thead>
    //                                                        <tr>
    //                                                            <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Data</th>
    //                                                            <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Hora</th>
    //                                                            <th class="p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell">Paciente</th>
    //                                                        </tr>
    //                                                    </thead>
    //                                                    {this.state.consultasAgendadas.map(consulta =>
    //                                                        <tr>
    //                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{consulta.data}</td>
    //                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">{consulta.hora}</td>
    //                                                            <td class="p-3 font-semibold border-top border-gray-300 hidden lg:table-cell">Sr(a). {consulta.paciente}</td>
    //                                                        </tr>)}
    //                                                </table>
    //                                            </div>
    //                                        </div>
    //                                    </div>
    //                                </div>

    //                            </div>
    //                        </div>
    //                    </div>
    //                    </section>
    //                </main>
    //            <RodapeConta />
    //        </>
    //    )
    //}
}