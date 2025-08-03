-- tabela Usuario
CREATE TABLE application.usuario (
    Id SERIAL PRIMARY KEY,
    Nome TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    Senha TEXT NOT NULL,
    Role TEXT NOT NULL
);

-- tabela Tarefa
CREATE TABLE application.tarefa (
    Id SERIAL PRIMARY KEY,
    Titulo TEXT NOT NULL,
    Corpo TEXT NOT NULL,
    DataFinal TIMESTAMP NOT NULL,
    UsuarioId INTEGER NOT NULL,
    CONSTRAINT fk_usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) ON DELETE CASCADE,
    Status TEXT NOT NULL DEFAULT 'PROGRESSO'
);
