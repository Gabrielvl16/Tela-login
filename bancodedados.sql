-- Criar o banco de dados
CREATE DATABASE IF NOT EXISTS Escola;
USE Escola;

-- Criar a tabela de Usuários para o login
CREATE TABLE IF NOT EXISTS Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Usuario VARCHAR(50) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL
);

-- Criar a tabela de Alunos para o cadastro
CREATE TABLE IF NOT EXISTS Alunos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    Curso VARCHAR(100) NOT NULL,
    Telefone VARCHAR(15) NOT NULL
);

-- Inserir um usuário padrão para login
INSERT INTO Usuarios (Usuario, Senha) VALUES ('admin', 'admin');
