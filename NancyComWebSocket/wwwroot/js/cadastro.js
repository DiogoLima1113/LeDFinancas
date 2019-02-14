var cadastroDeTitulos = (function(){
    let dados = {};

    $(document).ready(function () {
       obterDadosParaCadastro();
    });

    function atualizarTabela(){
        $('#tabela-titulos').DataTable({
            language: {
                url: "Content/js/json/Portuguese-Brasil.json"
            },
            data: formatarDadosParaTabela(dados),
            columns: [
                {data:"tituloId", title: "Id", class:"column-center" },
                {data:"clienteNome", title: "Cliente", class:"column-left"},
                {data:"numero", title: "Número do Título", class:"column-left"},
                {data:"tipo", title: "Tipo", class:"column-center"},
                {data:"descricao", title: "Descrição",class:"column-left" },
                {data:"naturezaLancamentoDescricao", title: "Natureza de Lançamento", class:"column-left"},
                {data:"valor", title: "Valor", class:"column-right"},
                {data:"dataReferencia", title:"Data de Referencia", class:"column-center"},
                {data:"dataVencimento", title: "Data de Vencimento", class:"column-center"}
            ]
        });
    }


    function formatarDadosParaTabela(dados){
        var dadosDoTituloFormatado = [];

        dados.titulos.forEach((t) => {
            var obj = {
                tituloId: t.Id,
                clienteId: t.UsuarioId,
                clienteNome: dados.clientes.find(c => c.Id == t.UsuarioId).Login,
                naturezaLancamentoDescricao: dados.naturezas.find(n => n.Id == t.NaturezaLancamentoId).Descricao,
                naturezaLancamentoId: t.NaturezaLancamentoId,
                numero: t.Numero,
                descricao: t.Descricao,
                valor: "R$ " + t.Valor,
                dataCadastro: t.DataCadastro,
                dataReferencia: t.DataReferencia,
                dataVencimento: t.DataVencimento,
                dataInativacao: t.DataInativacao,
                tipo: (t.Tipo == "p") ? "Pagamento" : "Recebimento",
                observacao: t.Observacao
            }
            dadosDoTituloFormatado.push(obj);
        });
        return dadosDoTituloFormatado;
    }

    function obterDadosParaCadastro(){
        $.ajax({
            url: "/dadosParaCadastro",
            type: 'GET',
            contentType: 'application/json',
            success: function(response,status,xhr) {
                dados = JSON.parse(response);
                atualizarTabela();
            },
            error: function(error){
                console.log(error);
                alert("ops, erro!")
            } 
        });
    }
    
})();


