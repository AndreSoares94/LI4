import React, { Component } from 'react';
import { Link } from 'react-router-dom';

import Medicos from './images/medicos.png';
import Casa1 from './images/casas3.png';
import Casa2 from './images/casas4.jpg';
import Casa3 from './images/triangulo_casas.png';
import Estetoscopio from './images/estetoscopio.png';
import Trust from './images/trust.jpg';
import Logo from './images/logo_consultaJa.png';
import LogoS from './images/logo.png';
import Fundo from './images/fundo.jpg'
import { Rodape } from './Rodape.js';
import { NavBarOut } from './NavBarOut.js';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'


export class Landing extends Component {
    static displayName = Landing.name;

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            age: null,
        };
    }

    mySubmitHandler = (event) => {
        event.preventDefault();
        let age = this.state.age;
        if (!Number(age)) {
            alert("Your age must be a number");
        }
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    myFunction = () => {
        var x = document.getElementById("myInput");
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    }

    render() {
        return (
            <>
                <NavBarOut />
                <main>
                    <div className="relative pt-16 pb-32 flex content-center items-center justify-center"
                        style={{
                            minHeight: "60vh"
                        }}>
                        <div className="absolute top-0 w-full h-full bg-center bg-cover"
                            style={{
                                backgroundImage: "url('https://images.unsplash.com/photo-1532938911079-1b06ac7ceec7?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1189&q=80')"
                            }}>
                            <span id="blackOverlay" className="w-full h-full absolute opacity-75 bg-black"></span>
                        </div>
                        <div className="container relative mx-auto">
                            <div className="items-center flex flex-wrap">
                                <div className="w-full lg:w-6/12 px-4 ml-auto mr-auto text-center">
                                    <div className="pr-12">
                                        <img
                                            alt="..."
                                            src={LogoS}
                                            className=" rounded-full mx-auto"
                                            style={{ maxWidth: "60px" }}
                                        />
                                        <h1 className="text-white font-semibold text-5xl">
                                            Consulta Já!
                                        </h1>
                                        <p className="mt-4 text-lg text-gray-300">
                                            Seja bem vindo à nossa plataforma de consultas ao domicílio.
                                            Crie uma conta e comece já a usufruir de todos os nossos serviços!
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div
                            className="top-auto bottom-0 left-0 right-0 w-full absolute pointer-events-none overflow-hidden"
                            style={{ height: "70px", transform: "translateZ(0)" }}
                        >
                            <svg
                                className="absolute bottom-0 overflow-hidden"
                                xmlns="http://www.w3.org/2000/svg"
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
                    </div>

                    <section className="pb-20 bg-gray-300 -mt-24">
                        <div className="container mx-auto px-4">
                            <div className="flex flex-wrap">
                                <div className="lg:pt-12 pt-6 w-full md:w-4/12 px-4 text-center">
                                    <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-8 shadow-lg rounded-lg">
                                        <div className="px-4 py-5 flex-auto">
                                            <div className="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 mb-5 shadow-lg rounded-full bg-red-500">
                                                <FontAwesomeIcon icon="briefcase-medical" />
                                            </div>
                                            <h6 className="text-xl font-semibold">Os Nossos Serviços</h6>
                                            <p className="mt-2 mb-4 text-gray-600">
                                                Consultas ao domicílio, combinadas entre médicos e 
                                                clientes/pacientes, de forma a garantir o maior conforto aos nossos clientes.
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div className="w-4/12 px-4 text-center">
                                    <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-8 shadow-lg rounded-lg">
                                        <div className="px-4 py-5 flex-auto">
                                            <div className="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 mb-5 shadow-lg rounded-full bg-blue-700">
                                                <FontAwesomeIcon icon="user-md" />
                                            </div>
                                            <h6 className="text-xl font-semibold">
                                                Quem Somos?
                                            </h6>
                                            <p className="mt-2 mb-4 text-gray-600">
                                                Somos uma empresa empenhada em fornecer os melhores cuidados de saúde possiveis à população em geral sem a
                                                necessidade de se descolarem e esperarem em grandes filas.
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div className="pt-6 w-full md:w-4/12 px-4 text-center">
                                    <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-8 shadow-lg rounded-lg">
                                        <div className="px-4 py-5 flex-auto">
                                            <div className="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 mb-5 shadow-lg rounded-full bg-green-500">
                                                <FontAwesomeIcon icon="fingerprint" />
                                            </div>
                                            <h6 className="text-xl font-semibold">
                                                O que fazemos?
                                            </h6>
                                            <p className="mt-2 mb-4 text-gray-600">
                                                O nosso objetivo é promover o bem-estar às pessoas da melhor forma possivel. Todos os dias trabalhamos para desenvolver novas ferramentas para oferecer mais recursos
                                                e funcionalidades deste projeto aos nossos clientes.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="flex flex-wrap items-center mt-32">
                                <div className="w-full md:w-5/12 px-4 mr-auto ml-auto">
                                    <div className="text-gray-600 p-3 text-center inline-flex items-center justify-center w-16 h-16 mb-6 shadow-lg rounded-full bg-gray-100">
                                        <FontAwesomeIcon icon="user-friends" />
                                    </div>
                                    <h3 className="text-3xl mb-2 font-semibold leading-normal">
                                        O Médico vai a sua casa!
                                    </h3>
                                    <p className="text-lg font-light leading-relaxed mt-4 mb-4 text-gray-700">
                                        Para quê esperar em filas de espera quando pode ser avaliado por um profissional em casa?
                                        Com o ConsultaJa pode requisitar uma consulta sem sair do conforto da sua habitação. 
                                    </p>
                                    <p className="text-lg font-light leading-relaxed mt-0 mb-4 text-gray-700">
                                        Inscreva-se já e começe a receber assitência disponível 24 horas por dia, 7 dias por semana.
                                    </p>
                                </div>
                                <div className="w-full md:w-4/12 px-4 mr-auto ml-auto">
                                    <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded-lg bg-pink-600">
                                        <img
                                            alt="..."
                                            src="https://images.unsplash.com/photo-1585139786570-905b59b4fddf?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1050&q=80"
                                            className="w-full align-middle rounded-t-lg"
                                        />
                                        <blockquote className="relative p-8 mb-4">
                                            <svg
                                                preserveAspectRatio="none"
                                                xmlns="http://www.w3.org/2000/svg"
                                                viewBox="0 0 583 95"
                                                className="absolute left-0 w-full block"
                                                style={{
                                                    height: "95px",
                                                    top: "-94px"
                                                }}
                                            >
                                                <polygon
                                                    points="-30,95 583,95 583,65"
                                                    className="text-pink-600 fill-current"
                                                ></polygon>
                                            </svg>
                                            <h4 className="text-xl font-bold text-black text-center">
                                                Serviços de qualidade garantida
                    </h4>
                                            <p className="text-md font-light mt-2 text-black text-center">
                                                Os profissionais de saúde do ConsultaJa estão preparados para qualquer tipo de situação e são aprovados e referenciados pela Ordem dos Médicos.
                    </p>
                                        </blockquote>
                                    </div>
                                </div>

                            </div>
                            </div>
                    </section>

                    <section className="pb-20 relative block bg-gray-900">
                        <div
                            className="bottom-0 top-0 left-0 right-0 w-full absolute pointer-events-none overflow-hidden -mt-20"
                            style={{ height: "80px", transform: "translateZ(0)" }}
                        >
                            <svg
                                className="absolute bottom-0 overflow-hidden"
                                xmlns="http://www.w3.org/2000/svg"
                                preserveAspectRatio="none"
                                version="1.1"
                                viewBox="0 0 2560 100"
                                x="0"
                                y="0"
                            >
                                <polygon
                                    className="text-gray-900 fill-current"
                                    points="2560 0 2560 100 0 100"
                                ></polygon>
                            </svg>
                        </div>

                        </section>
                </main>
                <Rodape />
            </>
        );
    }
}
