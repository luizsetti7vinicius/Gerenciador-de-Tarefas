# Gerenciador de Tarefas

Aplicação de console desenvolvida em C# para cadastrar e acompanhar tarefas, utilizando `List<Tarefa>` para armazenar as tarefas e `Stack<int>` para manter o histórico das conclusões.

## Funcionalidades

- Cadastrar tarefas com identificador automático.
- Listar todas as tarefas e suas situações.
- Buscar uma tarefa pelo identificador.
- Marcar uma tarefa como concluída.
- Desfazer a última conclusão registrada.
- Validar entradas inválidas e impedir a conclusão duplicada de uma tarefa.

## Estruturas de dados utilizadas

- **List<Tarefa>**: mantém a coleção de tarefas cadastradas.
- **Stack<int>**: armazena os identificadores das tarefas concluídas. Como a pilha segue LIFO (Last In, First Out), a última conclusão registrada é a primeira que pode ser desfeita.

## Como executar

É necessário ter o **.NET 8 SDK** ou uma versão compatível mais recente instalada.

1. Abra esta pasta no Visual Studio Code.
2. Abra o terminal integrado (`Terminal` > `New Terminal`).
3. Execute:

```bash
dotnet run
```

Os dados ficam armazenados em memória e são perdidos quando o programa é encerrado.
