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
    barcode: { textAlign: 'center', width: 80, height: 30 },
    textImage: { fontSize: 60, textAlign: 'left' },
    numReceita: { fontSize: 14, left: 320, top: -80 },
    page: { padding: 40 },
    box: { width: '100%', marginBottom: 30, borderRadius: 5 },
    utente: { width: '50%', marginBottom: 0, borderRadius: 5, left: 20, top: -120 },
    medico: { width: '50%', marginBottom: 0, borderRadius: 5, left: 310, top: -120 },
    info: { fontSize: 12, textAlign: 'justify', paddingTop: 14, color: 'black' },
    descricaoT: { fontSize: 14, textAlign: 'justify', color: 'black', left: 20, top: -50 },
    descricao: { fontSize: 12, textAlign: 'justify', color: 'black', paddingTop: 6, left: 20, top: -50 },
    pageNumbers: {
        position: 'absolute',
        bottom: 40,
        left: 0,
        right: 0,
        fontSize: 14,
        color: 'black',
        textAlign: 'center',
    },
    container: {
        flexDirection: 'row',
        borderTopColor: borderColor,
        borderTopWidth: 1,
        borderDownColor: borderColor,
        borderDownWidth: 1,
        height: 20,
        textAlign: 'center',
        fontStyle: 'bold',
        flexGrow: 1,
    },
    description: {
        width: '25%',
        height: 20,
        borderLeftColor: borderColor,
        borderRightWidth: 1,
        fontSize: 14,
    },
    qty: {
        width: '15%',
        height: 20,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        fontSize: 14,
    },
    rate: {
        width: '35%',
        height: 20,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        fontSize: 14,
    },
    amount: {
        width: '25%',
        height: 20,
        fontSize: 14,
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
        width: '25%',
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
        width: '15%',
        height: 40,
        borderRightColor: borderColor,
        borderRightWidth: 1,
        textAlign: 'center',
        paddingRight: 2,
        paddingTop: 4,
        fontSize: 12,
    },
    raterow: {
        width: '35%',
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
});

export class Receita extends Component {
    static displayName = Receita.name;

    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            observacoes: '',
            prescricoes: [],
            utente: '',
            contactout: '',
            nifut: '',
            medico: '',
            contactomed: '',
            nifmed: ''
        };
    }

    componentDidMount() {
        const idC = localStorage.getItem('consulta');
        //localStorage.removeItem('consulta');
        console.log("IdConsulta " + idC);
        this.setState({ id: idC });
        // Buscar os dados de uma receita
        api.get(`/consultas/receitas`, {
            params: {
                id: idC
            }
        })
            .then(res => {
                console.log(res);
                this.setState({
                    observacoes: res.data.observacoes,
                    prescricoes: res.data.prescricoes,
                    utente: res.data.utente,
                    medico: res.data.medico,
                    contactout: res.data.contactoUt,
                    contactomed: res.data.contactoMed,
                    nifut: res.data.nifUt,
                    nifmed: res.data.nifMed
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
                        <Text style={styles.numReceita} fixed>
                            Receita Médica nº {this.state.id}
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
                        </View>
                    </View>

                    <Text style={styles.descricaoT}>
                        Observações:
      </Text>

                    <Text style={styles.descricao}>
                        {this.state.observacoes}
      </Text>

                    <View style={styles.container}>
                        <Text style={styles.description}>Nome</Text>
                        <Text style={styles.qty}>Quantidade</Text>
                        <Text style={styles.rate}>Posologia</Text>
                        <Text style={styles.amount}>Código</Text>
                    </View>
                    {this.state.prescricoes.map(prescricao =>
                        <View style={[styles.row, { top: -(455 - 40 * this.state.prescricoes.length) }]} >
                            <Text style={styles.descriptionrow}>{prescricao.nome}</Text>
                            <Text style={styles.qtyrow}>{prescricao.quantidade}</Text>
                            <Text style={styles.raterow}>{prescricao.posologia}</Text>
                            <Text style={styles.amountrow}> <Image style={styles.barcode} src={ Barcode }/> </Text>
                        </View>
                    )}
                    <Text style={styles.pageNumbers} render={({ pageNumber, totalPages }) => (
                        `${pageNumber} / ${totalPages}`
                    )} fixed />
                </Page>
            </Document>
        )
    }
}

