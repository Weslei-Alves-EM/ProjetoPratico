# Projeto Cadastro de Aluno (CRUD)

Este projeto tem como objetivo desenvolver um sistema web para o cadastro de alunos, seguindo os requisitos e artefatos disponibilizados. O sistema permitirá operações básicas de cadastro, leitura, atualização e exclusão (CRUD) de alunos, além de fornecer funcionalidades relacionadas ao cadastro de cidades.

## Ferramentas

- **IDE:** [Microsoft Visual Studio Community 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)
- **Ferramenta para administração de banco de dados:** [DBeaver Community](https://dbeaver.io/download/)

## Tecnologias

- ASP.NET MVC (linguagem C#)
- SGBD (FIREBIRD): Não utilizar framework ORM
- HTML
- CSS (Boostrap)
- JS vanilla

## Observações

- Utilize recursos da linguagem como: Generics, LINQ, conversão de tipos, downcast, upcast, métodos de extensão.
- Não utilizar nenhum ORM como Entity Framework, Dapper, NHibernate.

## Requisitos

- A página inicial deve exibir uma lista contendo todos os alunos cadastrados, conforme ilustrado na imagem: TelaPrincipal.png.
- Deve ser disponibilizada uma interface única para realizar tanto o cadastro quanto a edição de alunos. Se o usuário estiver editando um aluno, o título da página deve mudar de "Adicionar Aluno" para "Edição de Aluno".
- Ao clicar no botão de exclusão, o sistema deve solicitar uma confirmação do usuário antes de realizar a exclusão do registro.
- O preenchimento do campo CPF não é obrigatório, mas, caso informado, o sistema deve validar se o CPF é válido.
- O campo de nome é obrigatório e deve ser alinhado à esquerda. Ele deve permitir entre 3 e 100 caracteres.
- O campo de sexo deve apresentar apenas duas opções: Masculino e Feminino, e deve ser de apenas leitura, ou seja, o usuário não pode inserir outra informação.
- O campo CPF não é obrigatório. No entanto, se fornecido, deve ser validado.
- A seleção do campo "Cidade" deve ser feita a partir de uma lista de cidades previamente cadastradas.
- Deve ser disponibilizada uma tela para o cadastro e edição de cidades. Novamente, é uma única tela, cujo título deve ser alterado para "Edição de Cidade" caso seja uma operação de edição.
- As informações sobre a cidade devem incluir o nome da cidade e a unidade federativa (UF).
- O sistema deve permitir a pesquisa de alunos por meio do número de matrícula ou pelo nome.

## Instruções de Execução

1. Clone este repositório.
2. Abra o projeto no Visual Studio Community 2022.
3. Configure o banco de dados Firebird usando o DBeaver ou outra ferramenta de sua escolha.
4. Execute o projeto e explore as funcionalidades do sistema.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir problemas (issues) e enviar pull requests com melhorias.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
