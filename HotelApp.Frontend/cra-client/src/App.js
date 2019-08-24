import React from 'react';
import './App.css';

import ApolloClient from 'apollo-boost';
import { ApolloProvider } from '@apollo/react-hooks';

import Reservations from './components/Reservations';

const client = new ApolloClient({
    uri: 'http://localhost:53368/graphql',
});

function App() {
    return (
        <ApolloProvider client={client}>
            <div className="App">
                <h1>Hotel App</h1>
                <Reservations />
            </div>
        </ApolloProvider>
    );
}

export default App;
