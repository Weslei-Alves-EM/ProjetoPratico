document.addEventListener("DOMContentLoaded", function () {
    // Adiciona um evento de clique aos botões de rádio
    document.querySelectorAll('input[type="radio"]').forEach(function (radio) {
        radio.addEventListener("change", function () {
            // Define o valor do campo de busca com base no tipo de busca selecionado
            var placeholderText = "Digite a " + this.value;
            document.getElementById("valorBusca").setAttribute("placeholder", placeholderText);

            // Define o nome do campo de busca com base no tipo de busca selecionado
            var buscaType = "valorBusca"; // por padrão, usar o campo "valorBusca"
            if (this.value === "matricula") {
                buscaType = "matricula";
            } else if (this.value === "nome") {
                buscaType = "nome";
            } else if (this.value === "estado") {
                buscaType = "estado";
            }
            document.getElementById("valorBusca").setAttribute("name", buscaType);
        });
    });
});