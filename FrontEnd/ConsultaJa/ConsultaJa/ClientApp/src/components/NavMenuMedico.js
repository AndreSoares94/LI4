import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import logo from './images/logo_consultaJa.png';

export class NavMenuMedico extends Component {
    static displayName = NavMenuMedico.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <>
                <nav
                    className="top-0 absolute z-50 w-full items-center justify-between px-2 py-3 navbar-expand-sm"
                >
                    <div className="container px-16 mx-auto flex flex-wrap items-center justify-between">

                        <div className="w-full relative flex justify-between lg:w-auto lg:static lg:block lg:justify-start">
                        </div>
                        <div
                            className="w-full relative mt-8 flex justify-between lg:w-auto lg:static lg:block lg:justify-start"
                            id="example-navbar-warning"
                        >
                            <ul className="flex flex-col lg:flex-row list-none lg:ml-auto">

                                <li className="flex items-center">
                                    <Link tag={Link} className="links" to="/logout">
                                        <button
                                            className="bg-white text-gray-800 active:bg-gray-100 text-s font-bold uppercase px-4 py-2 rounded shadow hover:shadow-md outline-none focus:outline-none lg:mr-1 lg:mb-0 ml-3 mb-3"

                                            type="button"
                                            style={{ transition: "all .15s ease" }}
                                        >
                                            <i className="fas fa-arrow-alt-circle-down"></i> Logout
                </button>
                                    </Link>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </>
        );
    }
}