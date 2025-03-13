import { useEffect, useState } from "react";
import axios from "axios";
import NavBar from "../Components/Navbar";
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import styles from './styles.module.scss';
import dayjs from "dayjs";

const API_URL = "http://localhost:5116/venda";

export default function Venda() {
    const [vendas, setVendas] = useState([]);
    const [idCliente, setIdCliente] = useState("");
    const [idProduto, setIdProduto] = useState("");
    const [qtdVenda, setQtdVenda] = useState(0);
    const [vendaId, setVendaId] = useState("");
    const [nmClienteBusca, setNmClienteBusca] = useState("");
    const [dscProdutoBusca, setDscProdutoBusca] = useState("");

    useEffect(() => {
        fetchVendas();
    }, []);

    async function fetchVendas() {
        try {
            const params = {};

            if (nmClienteBusca) {
                params.nmCliente = nmClienteBusca;
            }
            if (dscProdutoBusca) {
                params.dscProduto = dscProdutoBusca;
            }

            const url = `${API_URL}/search`;

            const response = await axios.get(url, { params });
            setVendas(response.data);
            console.log(response.data);
        } catch (error) {
            console.error("Erro ao buscar vendas", error);
        }
    }

    function handleSearch() {
        fetchVendas();
    }

    async function handleSubmit(e) {
        e.preventDefault();
        const dthVenda = new Date().toISOString();
        const venda = { idCliente, idProduto, qtdVenda, dthVenda };
        try {
            if (vendaId) {
                await axios.put(`${API_URL}/${vendaId}`, venda);
            } else {
                await axios.post(API_URL, venda);
            }
            fetchVendas();
            setIdCliente("");
            setIdProduto("");
            setQtdVenda(0);
            setVendaId("");
        } catch (error) {
            console.error("Erro ao salvar venda", error);
        }
    }

    async function deleteVenda(id) {
        const confirmDelete = window.confirm("Tem certeza que deseja excluir esta venda?");
        if (confirmDelete) {
            try {
                await axios.delete(`${API_URL}/${id}`);
                fetchVendas();
            } catch (error) {
                console.error("Erro ao deletar venda", error);
            }
        }
    }

    function handleClear() {
        setIdCliente("");
        setIdProduto("");
        setQtdVenda(0);
        setVendaId("");
        setNmClienteBusca("");
        setDscProdutoBusca("");
    }

    async function importarVendas() {
        try {
            await axios.post(`${API_URL}/importar`);
            fetchVendas();
        } catch (error) {
            console.error("Erro ao importar vendas", error);
        }
    }

    return (
        <>
            <NavBar />
            <div className={styles.container}>
                <Button variant="success" onClick={importarVendas}>
                    Importar Vendas
                </Button>
                <h2>Gerenciamento de Vendas</h2>
                <Card className={styles.card}>
                    <Card.Body>
                        <Form onSubmit={handleSubmit}>
                            <Form.Label>ID Cliente</Form.Label>
                            <Form.Control
                                value={idCliente}
                                type="text"
                                placeholder="ID do Cliente"
                                onChange={(e) => setIdCliente(e.target.value)}
                            />

                            <Form.Label>ID Produto</Form.Label>
                            <Form.Control
                                value={idProduto}
                                type="text"
                                placeholder="ID do Produto"
                                onChange={(e) => setIdProduto(e.target.value)}
                            />

                            <Form.Label>Quantidade</Form.Label>
                            <Form.Control
                                value={qtdVenda}
                                type="number"
                                placeholder="Quantidade"
                                onChange={(e) => setQtdVenda(e.target.value)}
                            />

                            <div className={styles.buttonContainer}>
                                <Button variant="primary" type="submit">
                                    {vendaId ? "Atualizar" : "Criar"}
                                </Button>
                                <Button variant="secondary" type="button" onClick={handleClear}>
                                    Limpar
                                </Button>
                            </div>
                        </Form>
                    </Card.Body>
                </Card>

                <h3>Busca de Vendas</h3>
                <div className={styles.searchContainer}>
                    <Form.Group className="mb-3" controlId="searchCliente">
                        <Form.Label>Nome do Cliente</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Buscar por cliente"
                            value={nmClienteBusca}
                            onChange={(e) => setNmClienteBusca(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="searchProduto">
                        <Form.Label>Descrição do Produto</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Buscar por produto"
                            value={dscProdutoBusca}
                            onChange={(e) => setDscProdutoBusca(e.target.value)}
                        />
                    </Form.Group>

                    <Button variant="primary" type="button" onClick={handleSearch}>Buscar</Button>
                </div>

                <h3>Lista de Vendas</h3>
                <ul>
                    {vendas.map(venda => {
                        const valorTotal = venda.vlrTotalVenda;
                        const dataVenda = dayjs(venda.dthVenda).format('DD/MM/YYYY HH:mm:ss');
                        return (
                            <li key={venda.idVenda}>
                                {venda.idCliente} - {venda.idProduto} - {venda.qtdVenda} unidades - Valor Total: R${valorTotal?.toFixed(2)} - Data: {dataVenda}
                                <div className={styles.buttonActions}>
                                    <Button
                                        variant="warning"
                                        onClick={() => {
                                            setVendaId(venda.idVenda);
                                            setIdCliente(venda.idCliente);
                                            setIdProduto(venda.idProduto);
                                            setQtdVenda(venda.qtdVenda);
                                            window.scrollTo({ top: 0, behavior: 'smooth' });
                                        }}
                                    >
                                        Editar
                                    </Button>
                                    <Button variant="danger" onClick={() => deleteVenda(venda.idVenda)}>
                                        Deletar
                                    </Button>
                                </div>
                            </li>
                        );
                    })}
                </ul>
            </div>
        </>
    );
}
