import { useEffect, useState } from "react";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";
import NavBar from "../Components/Navbar";
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import styles from './styles.module.scss';

const API_URL = "http://localhost:5116/produto";

export default function Produto(){
    const [produtos, setProdutos] = useState([]);
    const [dscProduto, setDscProduto] = useState("");
    const [vlrUnitario, setVlrUnitario] = useState(0.0);
    const [produtoId, setProdutoId] = useState("");

    useEffect(() => {
        fetchProdutos();
    }, []);
    
    async function fetchProdutos() {
        try {
            const response = await axios.get(API_URL);
            setProdutos(response.data);
            console.log(response);
        } catch (error) {
            console.error("Erro ao buscar produtos", error);
        }
    }

    async function handleSubmit(e) {
        e.preventDefault();
        const produto = { dscProduto: dscProduto, vlrUnitario: vlrUnitario };
        try {
            if(produtoId) {
                await axios.put(`${API_URL}/${produtoId}`, produto);
            } else {
                await axios.post(API_URL, produto);
            }
            fetchProdutos();
            setDscProduto("");
            setVlrUnitario(0.0);
            setProdutoId("");
        } catch (error) {
            console.error("Erro ao salvar produto", error);
        }
    }

    async function deleteProduto(id) {
        const confirmDelete = window.confirm("Tem certeza que deseja excluir este produto?");
        if(confirmDelete) {
            try {
                await axios.delete(`${API_URL}/${id}`);
                fetchProdutos();
            } catch (error) {
                console.error("Erro ao deletar produto", error);
            }
        }
    }

    function handleClear() {
        setDscProduto("");
        setVlrUnitario("");
        setProdutoId("");
    }

    return(
        <>
            <NavBar />
            <div className={styles.container}>
                <h2>Gerenciamento de Produtos</h2>
                <Card className={styles.card}>
                    <Card.Body>
                        <Form onSubmit={handleSubmit}>
                            <Form.Label>Descrição</Form.Label>
                            <Form.Control
                                value={dscProduto}
                                type="text"
                                placeholder="Descrição"
                                onChange={(e) => setDscProduto(e.target.value)}
                            />

                            <Form.Label>Valor unitário</Form.Label>
                            <Form.Control
                                value={vlrUnitario}
                                type="number"
                                placeholder="0.0"
                                onChange={(e) => setVlrUnitario(e.target.value)}
                            />

                            <div className={styles.buttonContainer}>
                                <Button variant="primary" type="submit">
                                    {produtoId ? "Atualizar" : "Criar"}
                                </Button>
                                <Button variant="secondary" type="button" onClick={handleClear}>
                                    Limpar
                                </Button>
                            </div>
                        </Form>
                    </Card.Body>
                </Card>

                <h3>Lista de Produtos</h3>
                <ul>
                    {produtos.map(produto => (
                        <li key={produto.idProduto}>
                            {produto.dscProduto} - {produto.vlrUnitario}
                            <div className={styles.buttonActions}>
                                <Button
                                    variant="warning"
                                    onClick={() => {
                                        setProdutoId(produto.idProduto);
                                        setDscProduto(produto.dscProduto);
                                        setVlrUnitario(produto.vlrUnitario);
                                        window.scrollTo({ top: 0, behavior: 'smooth' });
                                    }}
                                >
                                    Editar
                                </Button>
                                <Button variant="danger" onClick={() => deleteProduto(produto.idProduto)}>
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