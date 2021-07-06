import React, { Component } from 'react';
import { Rodape } from './Rodape';
import { NavBarOut } from './NavBarOut';
import LogoS from './images/logo.png';

export class Logout extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        localStorage.removeItem("token");
        localStorage.removeItem("notify");
        if (localStorage.getItem("nome") != null) {
            localStorage.removeItem("nome");
        }
        const inter = localStorage.getItem('intervalo');
        localStorage.removeItem("intervalo");
        console.log("Intervalo = " + inter);
        clearInterval(inter);
    }

    handleSubmit = () => {

        this.props.history.push("/");
    }


    render() {
        return (
            <>
                <NavBarOut />
                <main>
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

                        <div className="container relative mx-auto h-1/2 mb-16 mt-16">
                            <div className="items-center flex flex-wrap">
                                <div className="w-6/12 px-4 ml-auto mr-auto text-center">
                                    <div className="pr-12">
                                        <img
                                            alt="..."
                                            src={LogoS}
                                            className=" rounded-full mx-auto"
                                            style={{ maxWidth: "60px" }}
                                        />
                                        <h1 className="text-black font-semibold text-5xl">
                                            Consulta Já!
                                        </h1>
                                        <p className="mt-4 text-lg text-black">
                                            Obrigado por utilizar os nossos serviços!
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="container mx-auto px-4 h-1/2 pb-3">
                            <div className="flex content-center items-center justify-center h-full">
                                <div className="w-1/2 px-4">
                                    <div className="relative flex flex-col min-w-0 break-words w-full shadow-lg rounded-lg bg-gray-300 border-0">
                                        <div className="flex-auto px-4 lg:px-10 py-10 pt-0">
                                            <div className="text-gray-800 text-center mt-6 mb-3 font-bold">
                                                Volte Sempre!
                                            </div>
                                            <form onSubmit={this.handleSubmit}>
                                                <div className="text-center mt-6">
                                                    <button
                                                        className="bg-gray-900 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 w-full"
                                                        type="submit"
                                                        style={{ transition: "all .15s ease" }}
                                                    >
                                                        Voltar à Home Page
                                                    </button>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mt-48">
                            <Rodape />
                        </div>
                    </section>
                </main>
            </>

        );
    }
}