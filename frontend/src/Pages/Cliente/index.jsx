import { useEffect, useState } from "react";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";
import NavBar from "../Components/Navbar";
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import styles from './styles.module.scss';

const API_URL = "http://localhost:5116/cliente";

export default function Cliente(){
    const [clientes, setClientes] = useState([]);
    const [nome, setNome] = useState("");
    const [cidade, setCidade] = useState("");
    const [clienteId, setClienteId] = useState("");

    useEffect(() => {
        fetchClientes();
    }, []);
    
    async function fetchClientes() {
        try {
            const response = await axios.get(API_URL);
            setClientes(response.data);
            console.log(response);
        } catch (error) {
            console.error("Erro ao buscar clientes", error);
        }
    }

    async function handleSubmit(e) {
        e.preventDefault();
        const cliente = { nmCliente: nome, cidade: cidade };
        try {
            if(clienteId) {
                await axios.put(`${API_URL}/${clienteId}`, cliente);
            } else {
                await axios.post(API_URL, cliente);
            }
            fetchClientes();
            setNome("");
            setCidade("");
            setClienteId("");
        } catch (error) {
            console.error("Erro ao salvar cliente", error);
        }
    }

    async function deleteCliente(id) {
        const confirmDelete = window.confirm("Tem certeza que deseja excluir este cliente?");
        if(confirmDelete) {
            try {
                await axios.delete(`${API_URL}/${id}`);
                fetchClientes();
            } catch (error) {
                console.error("Erro ao deletar cliente", error);
            }
        }
    }

    function handleClear() {
        setNome("");
        setCidade("");
        setClienteId("");
    }

    async function importarClientes() {
        try {
            await axios.post(`${API_URL}/importar`);
            fetchClientes();
            alert("Clientes importados com sucesso!");
        } catch (error) {
            console.error("Erro ao importar clientes", error);
            alert("Erro ao importar clientes");
        }
    }

    return(
        <>
            <NavBar />
                <div className={styles.container}>
                <Button variant="success" onClick={importarClientes}>
                    Importar Clientes
                </Button>
                <h2>Gerenciamento de Clientes</h2>
                <Card className={styles.card}>
                    <Card.Body>
                        <Form onSubmit={handleSubmit}>
                            <Form.Label>Nome</Form.Label>
                            <Form.Control
                                value={nome}
                                type="text"
                                placeholder="Nome"
                                onChange={(e) => setNome(e.target.value)}
                            />

                            <Form.Label>Cidade</Form.Label>
                            <Form.Control
                                value={cidade}
                                type="text"
                                placeholder="Cidade"
                                onChange={(e) => setCidade(e.target.value)}
                            />

                            <div className={styles.buttonContainer}>
                                <Button variant="primary" type="submit">
                                    {clienteId ? "Atualizar" : "Criar"}
                                </Button>
                                <Button variant="secondary" type="button" onClick={handleClear}>
                                    Limpar
                                </Button>
                            </div>
                        </Form>
                    </Card.Body>
                </Card>

                <h3>Lista de Clientes</h3>
                <ul>
                    {clientes.map(cliente => (
                        <li key={cliente.idCliente}>
                            {cliente.nmCliente} - {cliente.cidade}
                            <div className={styles.buttonActions}>
                                <Button
                                    variant="warning"
                                    onClick={() => {
                                        setClienteId(cliente.idCliente);
                                        setNome(cliente.nmCliente);
                                        setCidade(cliente.cidade);
                                        window.scrollTo({ top: 0, behavior: 'smooth' });
                                    }}
                                >
                                    Editar
                                </Button>
                                <Button variant="danger" onClick={() => deleteCliente(cliente.idCliente)}>
                                    Deletar
                                </Button>
                            </div>
                        </li>
                    ))}
                </ul>
            </div>
        </>
    )
}