import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

import { Rodape } from './Rodape';
import { NavBarOut } from './NavBarOut';

export class Privacy extends Component {
    static displayName = Privacy.name;

    render() {
        return (
            <>
                <NavBarOut />
                <main>
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
                    <section className="pb-20 bg-gray-300 -mt-24">
                        <div className="container mx-auto px-4">
                        <h1 className="text-4xl font-semibold leading-normal mb-4 text-gray-800 mb-2"> Privacy </h1>
                        <ol class="list-decimal">
                            <li className="text-black font-semibold text-5xl">
                                <h3 className="text-black font-semibold text-5xl"> Questões</h3>
                                <p className="mt-4 text-lg text-black-300">Caso tenha qualquer questão relacionada com o tratamento dos seus dados pessoais 
                                        e com o exercício dos direitos que lhe são conferidos pela legislação aplicável 
                                        contacte-nos através dos seguintes contactos:
                                </p>
                                    <p className="mt-4 text-lg text-black-300">E-mail: <a className="text-blue-300" href="mailto:consultaja4@gmail.com">consultaja4@gmail.com</a></p>
                            </li>

                            <li className="text-black font-semibold text-5xl">
                                <h3 className="text-black font-semibold text-5xl"> O que são dados pessoais?</h3>
                                <p className="mt-4 text-lg text-black-300">Dados pessoais são qualquer informação, de qualquer natureza e independentemente 
                                   do respetivo suporte, incluindo som e imagem, relativa a uma pessoa singular identificada 
                                   ou identificável.</p> 
                                <p className="mt-4 text-lg text-black-300"> É considerada identificável a pessoa singular que possa ser identificada, 
                                    direta ou indiretamente, designadamente por referência a um nome, número de identificação, 
                                    dados de localização, identificadores por via eletrónica ou a um ou mais elementos específicos 
                                    da sua identidade física, fisiológica, genética, mental, económica, cultural ou social.
                                </p>
                            </li>

                            <li className="text-black font-semibold text-5xl">
                                <h3 className="text-black font-semibold text-5xl"> No que consiste o tratamento de dados pessoais?</h3>
                                <p className="mt-4 text-lg text-black-300"> O tratamento de dados pessoais consiste numa operação ou conjunto 
                                   de operações efetuadas sobre dados pessoais ou conjuntos de dados pessoais, 
                                   através de meios automatizados, ou não, nomeadamente a recolha, o registo, 
                                   a organização, a estruturação, a conservação, a adaptação, a recuperação, 
                                   a consulta, a utilização, a divulgação, difusão, comparação, interconexão, 
                                   a limitação, o apagamento ou a destruição. </p>
                            </li>
                            </ol>
                        </div>
                    </section>
                </main>
                <Rodape />
            </>
        );
    }
}
