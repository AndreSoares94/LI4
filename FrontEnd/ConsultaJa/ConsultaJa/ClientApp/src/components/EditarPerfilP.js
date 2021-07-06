import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import decode from 'jwt-decode';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import logo from './images/logo_consultaJa.png';

import Medicos from './images/medicos.png';
import api from './api';
import { NavMenuPaciente } from './NavMenuPaciente';
import { RodapeConta } from './RodapeConta';

export class EditarPerfilP extends Component {
    static displayName = EditarPerfilP.name;

    constructor(props) {
        super(props);

        this.state = {
            id: '',
            data: '',
            hora: '',
            firstName: '',
            isToggleOn: false,
            name: null,
            dataNascimento: null,
            morada: null,
            codigo_postal: null,
            passwordAntiga: null,
            passwordNova: null,
            nif: null,
            contactos: null,
            localidade: null,
            valInput: true
        };
    }

    componentDidMount() {
        const token = localStorage.getItem('token');
        var decoded = decode(token);
        const idD = decoded.Id;
        //console.log("Id" + idD);
        this.state.id = idD;
        this.setState({ firstName: localStorage.getItem("nome") });
        api.get(`consultas/precoCons`)
            .then(res => {
                this.setState({ precoCons: res.data })
            })
            .catch(err => { console.log(err) });
    }

    handleSubmit = (event) => {
        event.preventDefault();

        api.post(`consultas/regCons`, {
            Paciente: this.state.id,
            Data: this.state.data,
            Hora: this.state.hora
        })
            .then(res => {
                console.log(res);
                alert("Consulta Marcada à espera de resposta ");
                this.props.history.push("/perfilPaciente");
            })
            .catch(err => { console.log(err); alert("Erro na Marcação da Consulta"); });
    }

    verificaInputs = () => {

        if ((this.state.passwordNova == '' || this.state.passwordNova == null) &&
            (this.state.morada == '' || this.state.morada == null) &&
            (this.state.name == '' || this.state.name == null) &&
            (this.state.codigo_postal == '' || this.state.codigo_postal == null) &&
            (this.state.dataNascimento == '' || this.state.dataNascimento == null)) {
            this.state.valInput = false;
        } else {
            this.state.valInput = true;
        }
    }

    handleEdit = (event) => {
        event.preventDefault();

        this.verificaInputs();

        if (this.state.valInput == true) {
            api.put(`contas/${this.state.id}`, {
                Password: this.state.passwordAntiga,
                PasswordNova: this.state.passwordNova,
                Morada: this.state.morada,
                Nome: this.state.name,
                Codigo_postal: this.state.codigo_postal,
                DataNascimento: this.state.dataNascimento
            })
                .then(res => {
                    console.log(res);
                    alert("Perfil editado com sucesso ");
                    this.setState({ isToggleOn: false });
                    if (this.state.name != null && this.state.name != '') {
                        localStorage.setItem("nome", this.state.name);
                        this.setState({ firstName: this.state.name });
                    }
                    this.props.history.push("/perfilPaciente");
                })
                .catch(err => {
                    console.log(err)
                    alert("Não foi possível editar perfil");
                    this.props.history.push("/perfilPaciente");
                });
        } else {
            alert("Não foi possível editar perfil");
        }
    }

    handleOnAccept = () => {
        (this.state.isToggleOn) ? this.setState({ isToggleOn: false }) : this.setState({ isToggleOn: true });
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

                    {/* Brand */}
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

                    {/* Navigation */}
                    <ul className="md:flex-col md:min-w-full flex flex-col list-none">
                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/perfilPaciente"
                            >
                                <FontAwesomeIcon icon="user" /><i className="text-gray-500 mr-2 text-sm"></i> Perfil
                                </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/propostasConsultaP"
                            >
                                <FontAwesomeIcon icon="syringe" /> <i className="opacity-75 mr-2 text-sm"></i> Propostas de Consulta
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/marcarConsulta"
                            >
                                <FontAwesomeIcon icon="paste" /><i className="text-gray-500 mr-2 text-sm"></i> Marcar Consulta
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/historicoPaciente"
                            >
                                <FontAwesomeIcon icon="clipboard-list" /><i className="text-gray-500 mr-2 text-sm"></i> Histórico de Consultas
                                    </Link>
                        </li>

                        <li className="items-center">
                            <Link
                                className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
                                to="/editarPerfilP"
                            >
                                <FontAwesomeIcon icon="edit" /><i className="text-gray-500 mr-2 text-sm"></i> Editar Perfil
                                    </Link>
                        </li>
                    </ul>
                    {/* Divider */}
                    <hr className="my-4 md:min-w-full" />
                </nav>

                <main className="relative md:ml-64 h-full historico-page">
                    <NavMenuPaciente />
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
                    <section className="bg-gray-300">
                        <div className="container mx-auto px-4 h-full">
                            <div className="flex content-center items-center justify-center h-full">
                                <div className="w-1/2 lg:w-8/12 px-4 -mt-64 mb-16">
                                    <div className="relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded-lg bg-gray-300 border-0">

                                        <div className="flex-auto px-4 lg:px-24 py-12 pt-0">
                                            <div className="block text-gray-700 uppercase text-4xl text-center m-4 mb-3 font-semibold">
                                                <large>Editar Perfil</large>
                                            </div>
                                            <div class="flex-auto px-4 lg:px-24 py-12 pt-0 content-center items-center justify-center">
                                                <form onSubmit={this.handleEdit}>
                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                name='name'
                                                                type="text"
                                                                placeholder="Nome"
                                                                onChange={this.myChangeHandler} />
                                                        </div>
                                                    </div>

                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                type='date'
                                                                name='dataNascimento'
                                                                placeholder="Data de Nascimento"
                                                                onChange={this.myChangeHandler}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                type="password"
                                                                name='passwordAntiga'
                                                                placeholder="Password Atual"
                                                                onChange={this.myChangeHandler}
                                                                required
                                                            />
                                                        </div>
                                                    </div>

                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                type="password"
                                                                name='passwordNova'
                                                                placeholder="Nova Password"
                                                                onChange={this.myChangeHandler}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                type="text"
                                                                name='morada'
                                                                placeholder="Morada"
                                                                onChange={this.myChangeHandler}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <div class="w-4/6 px-3">
                                                            <input
                                                                class="appearance-none block w-full bg-gray-100 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                                                type="text"
                                                                name='codigo_postal'
                                                                placeholder="XXXX-XXX"
                                                                onChange={this.myChangeHandler}
                                                            />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="flex flex-wrap -mx-3 content-center items-center justify-center">
                                                        <input class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-3 px-8 rounded mb-1"
                                                            type='submit'
                                                            value="Editar" />
                                                    </div>
                                                </form></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <RodapeConta />
                    </section>
                </main>
            </>

        );
    }
}