import React, { Component } from 'react';
import axios from 'axios';

import { NavBarOut } from './NavBarOut';
import Medicos from './images/medicos.png';
import { CONTAS_URL } from './api';
import { Rodape } from './Rodape';

const InitialState = {
    codR: -1,
    codI: -1,
    isRegistarOn: true,
    name: '',
    dataNascimento: null,
    email: '',
    morada: '',
    codigo_postal: '',
    password: '',
    nif: '',
    contactos: '',
    localidade: '',
    type: ''
};

export class Registar extends Component {
    static displayName = Registar.name;

    constructor(props) {
        super(props);
        this.state = InitialState;
    }

    mySubmitHandler = (event) => {
        event.preventDefault();
        alert("Falta definir as acoes para os eventos");
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    // Enviar um mail e receber um codigo
    submitNew = (event) => {
        event.preventDefault();

        axios.get(`${CONTAS_URL}/email`, {
            params: {
                Email: this.state.email
            }
        })
            .then(conta => {
                this.setState({ codR: conta.data });
                (this.state.isRegistarOn) ? this.setState({ isRegistarOn: false }) : this.setState({ isRegistarOn: true });
            })
            .catch(err => {
                console.log(err)
                alert("Email já existente!")
            });
    }

    // Submeter um novo usuario, caso o codigo esteja bem
    handleCheck = (event) => {
        event.preventDefault();

        let codReg = this.state.codR;
        let codIns = this.state.codI;

        if ((parseInt(codReg.toString()) - parseInt(codIns.toString())) == 0) {
            axios.post(`${CONTAS_URL}`, {
                type: this.state.type,
                Nome: this.state.name,
                Email: this.state.email,
                Password: this.state.password,
                DataNascimento: this.state.dataNascimento,
                Morada: this.state.morada,
                Nif: this.state.nif,
                Codigo_postal: this.state.codigo_postal,
                Contactos: this.state.contactos,
                Localidade: this.state.localidade
            })
                .then(conta => {
                    //this.props.addUserToState(conta);
                    //this.props.toggle();
                    alert("Nova Conta Registada");
                })
                .catch(err => console.log(err));
        } else { alert("Código Inserido Inválido"); }
        this.setState(InitialState);
        (this.state.isRegistarOn) ? this.setState({ isRegistarOn: false }) : this.setState({ isRegistarOn: true });
    }

    render() {
        return (
            <>
                <NavBarOut />
                <main>
                {(this.state.isRegistarOn) ?

                    <section class="pb-20 bg-gray-300">
                            <div class="flex md:flex-row-reverse flex-wrap container mx-auto px-4 pt-32 pb-1 mb-auto">
                        <div class="w-full md:w-1/2">
                            <img class="w-full align-middle rounded-t-lg" src={Medicos} width="120" height="120" />
                        </div>

                        <div class="w-full md:w-1/2">
                            <form class="w-full max-w-lg" onSubmit={this.submitNew}>
                                    <p class="uppercase text-2xl tracking-wide text-gray-700 text-xs font-bold"> Tipo de Registo: </p>
                                <div className="radio">
                                        <label class="text-xl tracking-wide text-gray-700 text-xs cursor-pointer font-semibold">
                                            <input
                                            type="radio"
                                            name='type'
                                            value="Medico"
                                            onChange={this.myChangeHandler}
                                        />
                                        &ensp; Médico &emsp;
                                </label>
                                    <b />
                                        <label class="text-xl tracking-wide text-gray-700 cursor-pointer text-xs font-semibold">
                                        <input
                                            type="radio"
                                            name='type'
                                            value="Paciente"
                                            onChange={this.myChangeHandler}
                                        />
                                        &ensp; Paciente
                                </label>
                                </div>
                                <div class="flex flex-wrap -mx-3 ">
                                    <div class="w-full px-3 mb-6 md:mb-0">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-name">
                                            Nome
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="grid-name" name='name' type="text" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 ">
                                    <div class="w-full px-3 mb-6 md:mb-0">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-date">
                                            Data de Nascimento
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="grid-date" name='dataNascimento' type="date" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 ">
                                    <div class="w-full px-3">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-email">
                                            Email
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-email" name='email' type="email" placeholder="exemplo@email.com" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 ">
                                    <div class="w-full px-3">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-password">
                                            Password
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-password" name='password' type="password" placeholder="******************" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 ">
                                    <div class="w-full px-3">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-morada">
                                            Morada
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-morada" name='morada' type="text" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 mb-3">
                                            <div class="w-full md:w-1/2 px-3">
                                                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-cidade">
                                                    Distrito:
                                                    </label>
                                                <select class="block tracking-wide bg-gray-200 text-gray-700 border border-gray-200 py-3 px-3 mb-3 mt-2 focus:outline-none focus:bg-white focus:border-gray-500" id="grid-cidade" type="text" name='localidade' onChange={this.myChangeHandler} required>
                                                        <option className="block uppercase text-gray-700 text-l font-semibold mb-2" value="" disabled>Selecione uma opção</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Aveiro">Aveiro</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Beja">Beja</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Braga">Braga</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Bragança">Bragança</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Castelo Branco">Castelo Branco</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Coimbra">Coimbra</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Évora">Évora</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Faro">Faro</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Guarda">Guarda</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Leiria">Leiria</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Lisboa">Lisboa</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Portalegre">Portalegre</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Porto">Porto</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Santarém">Santarém</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Setúbal">Setúbal</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Viana do Castelo">Viana do Castelo</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Vila Real">Vila Real</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Viseu">Viseu</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Angra do Heroísmo">Angra do Heroísmo</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Funchal">Funchal</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Horta">Horta</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Lamego">Lamego</option>
                                                        <option className="block uppercase text-gray-700 text-l font-bold mb-2" value="Ponta Delgada">Ponta Delgada</option>
                                                    </select>
                                    </div>
                                    <div class="w-full md:w-1/2 px-3 md:mb-0">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
                                            Código-Postal
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-zip" type="text" placeholder="XXXX-XXX" name='codigo_postal' required pattern="\d{4}-\d{3}" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>
                                <div class="flex flex-wrap -mx-3 mb-2">
                                    <div class="w-full md:w-1/2 px-3">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-nif">
                                            NIF
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-nif" name='nif' type="text" onChange={this.myChangeHandler} required/>
                                    </div>
                                    <div class="w-full md:w-1/2 px-3">
                                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-contacto">
                                            Contacto
                                        </label>
                                                <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-contacto" name='contactos' type="text" onChange={this.myChangeHandler} required/>
                                    </div>
                                </div>

                                <button class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-3 px-4 rounded mb-10" type="submit">
                                    Registar
                                </button>
                            </form>
                            </div>
                        </div>
                    </section>
                        :
                        <section class="pb-20 bg-gray-300">
                            <div class="flex md:flex-row-reverse flex-wrap container mx-auto px-4 pt-32 pb-1 mb-auto">
                                <div class="w-full md:w-6/12">
                                    <img class="w-full align-middle rounded-t-lg" src={Medicos} width="120" height="120" />
                                </div>
                                <div class="w-full md:w-1/12"/>
                                <div class="w-full md:w-5/12">
                                <form onSubmit={this.handleCheck}>
                                    <p class="text-2xl tracking-wide text-gray-700 text-xs font-semibold"> Insira o Código de Registo enviado para o seu email {this.state.codR}</p>
                                    <input
                                        type="text"
                                        name='codI'
                                        class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                        placeholder="Insira o código..."
                                        onChange={this.myChangeHandler}
                                    />
                                    <br />
                                    <br />
                                        <button class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-3 px-4 rounded mb-10" type="submit">
                                            Submeter
                                        </button>
                                </form>
                            </div>
                            </div>
                        </section>
                    }
                    </main>
                <Rodape />
            </>
        );
    }
}
