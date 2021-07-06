import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { PDFViewer } from '@react-pdf/renderer';
import { Page, Text, View, Document, StyleSheet, Image, Font } from '@react-pdf/renderer';
import ReactPDF from '@react-pdf/renderer';
import Logo from './images/logo_consultaJa.png';
import Barcode from './images/barcode.png';
import api from './api';

const borderColor = 'black'

const styles = StyleSheet.create({
    image: { textAlign: 'center', marginVertical: 4, marginHorizontal: 10 },
    textImage: { fontSize: 60, textAlign: 'left' },
    numReceita: { fontSize: 14, left: 320, top: -90 },
    numReceita1: { fontSize: 14, left: 432, top: -110 },
    page: { padding: 40 },
    box: { width: '100%', marginBottom: 30, borderRadius: 5 },
    utente: { width: '50%', marginBottom: 0, borderRadius: 5, left: 20, top: -100 },
    medico: { width: '50%', marginBottom: 0, borderRadius: 5, left: 280, top: -100 },
    info: { fontSize: 12, textAlign: 'justify', paddingTop: 14, color: 'black' },
    descricaoT: { fontSize: 14, textAlign: 'left', color: 'black', left: 20, top: -50 },
    descricao: { fontSize: 12, textAlign: 'left', color: 'black', paddingTop: 6, left: 20, top: -50 },
    pageNumbers: {
        position: 'absolute',
        bottom: 20,
        left: 0,
        right: 0,
        textAlign: 'center'
    },
    container: {
        flexDirection: 'row',
        borderTopColor: borderColor,
        borderTopWidth: 1,
        borderDownColor: borderColor,
        borderDownWidth: 1,
        height: 40,
        textAlign: 'center',
        fontStyle: 'bold',
        flexGrow: 1,
    },
    description: {
        width: '40%',
        textAlign: 'center',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        borderLeftColor: borderColor,
        borderLefttWidth: 1,
        paddingLeft: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    qty: {
        width: '10%',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    rate: {
        width: '25%',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        paddingDown: 8,
        fontSize: 12,
    },
    amount: {
        width: '25%',
        height: 40,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    row: {
        flexDirection: 'row',
        borderTopColor: borderColor,
        borderTopWidth: 1,
        alignItems: 'top',
        height: 40,
        fontStyle: 'bold',
        fontSize: 12,
    },
    descriptionrow: {
        width: '40%',
        textAlign: 'center',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        borderLeftColor: borderColor,
        borderLefttWidth: 1,
        paddingLeft: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    qtyrow: {
        width: '10%',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    raterow: {
        width: '25%',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    amountrow: {
        width: '25%',
        height: 40,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    rowpreco: {
        flexDirection: 'row',
        alignItems: 'top',
        height: 40,
        fontStyle: 'bold',
        fontSize: 12,
    },
    descriptionrowpreco: {
        width: '40%',
        textAlign: 'center',
        height: 40,
        paddingLeft: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    qtyrowpreco: {
        width: '10%',
        height: 40,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    raterowpreco: {
        width: '25%',
        height: 40,
        borderRightColor: borderColor,
        borderRight: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 10,
        fontSize: 12,
    },
    amountrowpreco: {
        width: '25%',
        height: 40,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 10,
        fontSize: 12,
    },
});

export class Recibo extends Component {
    static displayName = Recibo.name;

    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            utente: '',
            contactout: '',
            nifut: '',
            medico: '',
            contactomed: '',
            nifmed: '',
            data: '',
            preco: '',
            distritoMed: '',
            distritoUt: '',
            codPostalMed: '',
            codPostalUt: ''
        };
    }

    componentDidMount() {
        const idC = localStorage.getItem('consulta');
        localStorage.removeItem('consulta');
        console.log("IdConsulta " + idC);
        this.setState({ id: idC });
        //Buscar os dados de uma receita
        api.get(`/consultas/recibos`, {
            params: {
                id: idC
            }
        })
            .then(res => {
                console.log(res);
                this.setState({
                    utente: res.data.utente,
                    medico: res.data.medico,
                    contactout: res.data.contactoUt,
                    contactomed: res.data.contactoMed,
                    nifut: res.data.nifUt,
                    nifmed: res.data.nifMed,
                    distritoMed: res.data.distritoMed,
                    distritoUt: res.data.distritoUt,
                    codPostalMed: res.data.codPostalMed,
                    codPostalUt: res.data.codPostalUt,
                    data: res.data.data,
                    preco: res.data.preco
                });
            })
            .catch(error => {
                alert("ERROR! " + error);
                console.log(error);
            });
    }

    render() {
        return (
            <Document>
                <Page style={styles.page} size="A4" fixed>
                    <View style={[styles.box, { width: 200, height: 80 }]} >
                        <Image
                            style={styles.image}
                            src={Logo}
                        />
                    </View>
                    <View style={[styles.box, { height: 80 }]} >
                        <Text style={styles.numReceita1} fixed>
                            Original
                        </Text>
                        <Text style={styles.numReceita} fixed>
                            Fatura/Recibo nº {this.state.id}/2020
                        </Text>
                    </View>
                    <View style={[styles.box, { height: 0, }]} >
                        <View style={[styles.utente, { height: 0 }]} >
                            <Text style={styles.info} >
                                Utente: {this.state.utente}
                            </Text>
                            <Text style={styles.info} >
                                Contacto: {this.state.contactout}
                            </Text>
                            <Text style={styles.info} >
                                Unidade Responsável: SNS
          </Text>
                            <Text style={styles.info} >
                                NIF: {this.state.nifut}
                            </Text>
                            <Text style={styles.info} >
                                {this.state.codPostalUt}    {this.state.distritoUt}
                            </Text>
                        </View>
                        <View style={[styles.medico, { height: 0 }]} >
                            <Text style={styles.info} break>
                                Médico: {this.state.medico}
                            </Text>
                            <Text style={styles.info} break>
                                Especialidade: Geral
          </Text>
                            <Text style={styles.info} break>
                                Contacto: {this.state.contactomed}
                            </Text>
                            <Text style={styles.info} break>
                                NIF: {this.state.nifmed}
                            </Text>
                            <Text style={styles.info} break>
                                {this.state.codPostalMed}    {this.state.distritoMed}
                            </Text>
                        </View>
                    </View>

                    <View style={styles.container}>
                        <Text style={styles.qty}>QTD</Text>
                        <Text style={styles.description}>Descrição</Text>
                        <Text style={styles.rate}>Preço por Unidade</Text>
                        <Text style={styles.amount}>Valor Total</Text>
                    </View>
                    <View style={[styles.row, { top: -310 }]} >
                        <Text style={styles.qtyrow}>1.</Text>
                        <Text style={styles.descriptionrow}>Consulta ao Domícilio</Text>
                        <Text style={styles.raterow}>{this.state.preco / 100}€</Text>
                        <Text style={styles.amountrow}>{this.state.preco / 100}€</Text>
                    </View>
                    <View style={[styles.row, { top: -310 }]} >
                        <Text style={styles.qtyrowpreco}></Text>
                        <Text style={styles.descriptionrowpreco}></Text>
                        <Text style={styles.raterowpreco}>Subtotal</Text>
                        <Text style={styles.amountrowpreco}>{(this.state.preco / 1.23 / 100).toFixed(2)}€</Text>
                    </View>
                    <View style={[styles.rowpreco, { top: -310 }]} >
                        <Text style={styles.qtyrowpreco}></Text>
                        <Text style={styles.descriptionrowpreco}></Text>
                        <Text style={styles.raterowpreco}>IVA 23%</Text>
                        <Text style={styles.amountrowpreco}>{((this.state.preco / 1.23) * 0.23 / 100).toFixed(2)}€</Text>
                    </View>
                    <View style={[styles.rowpreco, { top: -310 }]} >
                        <Text style={styles.qtyrowpreco}></Text>
                        <Text style={styles.descriptionrowpreco}></Text>
                        <Text style={styles.raterowpreco}>Total</Text>
                        <Text style={styles.amountrowpreco}>{this.state.preco/100}€</Text>
                    </View>

                    <Text style={styles.descricaoT}>
                        {this.state.data}
      </Text>
                    <Text style={styles.pageNumbers} render={({ pageNumber, totalPages }) => (
                        `${pageNumber} / ${totalPages}`
                    )} fixed />
                </Page>
            </Document>
        )
    }
}

