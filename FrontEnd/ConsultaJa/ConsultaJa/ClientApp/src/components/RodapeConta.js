import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class RodapeConta extends Component {
    static displayName = RodapeConta.name;

    render() {
        return (
            <footer class="relative bottom-0 bg-purple-100 w-full footer text-muted">
                <div className="container mx-auto px-4">
                    <hr className="border-gray-900" />
                    <div className="flex flex-wrap items-center md:justify-between justify-center">
                        <div className="w-full mx-auto text-center">
                            <div className="text-1xl text-gray-600 font-semibold py-2">
                                &copy; 2020 - ConsultaJa
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        );
    }
}
