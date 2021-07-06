import React, { Component } from 'react';
import { PDFDownloadLink, Document, Page } from '@react-pdf/renderer'
import { Receita } from './Receita'
import { Link, Redirect } from 'react-router-dom';

const Download = () => (
    <div className="flex w-1/2 content-center items-center justify-center">
    <div className="bg-blue-400 uppercase text-white font-bold hover:shadow-md shadow text-l px-auto py-2 mt-2 lg:w-2/5 cursor-pointer rounded outline-none focus:outline-none mb-4">
        <PDFDownloadLink document={<Receita />} fileName="receita.pdf">
            {({ blob, url, loading, error }) => (loading ? 'Loading document...' : 'Download Receita!')}
        </PDFDownloadLink>
        </div>
    </div>
);

export default Download;