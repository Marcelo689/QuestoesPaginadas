class SelectPagina {

    public static um = 0;
    public static dois = 1;
    public static quatro = 2;
    public static seis = 3;
    public static oito = 4;
    public static dez = 5;
    public static opcoes: SelectPagina = new SelectPagina();
}

class IndexController {

    public elementoNumeroPaginaAtual: JQuery;
    public elementoQuestoes: JQuery;
    public elementoNumeroQuestoesPorPagina: JQuery;

    public getTotalPaginas() {
        var quantidadeQuestoes: number = this.elementoQuestoes.length;
        var valueSelectQuestoesPorPagina: number = Number(this.elementoNumeroQuestoesPorPagina.val());
        var numeroPaginas :number = SelectPagina.opcoes[valueSelectQuestoesPorPagina];

        var numeroTotalPaginas = quantidadeQuestoes / numeroPaginas;
        return numeroTotalPaginas;
    }

    public proximaPagina() {
        var numeroPaginaAtual: number = Number(this.elementoNumeroPaginaAtual.val());
        var numeroTotalPaginas = this.getTotalPaginas();

        const paginaEhValidaParaAvancar = numeroPaginaAtual < numeroTotalPaginas;
        const proximaPagina = numeroPaginaAtual + 1;
        this.elementoNumeroPaginaAtual.val(proximaPagina);
    }

    public voltarPagina() {
        var numeroPaginaAtual: number = Number(this.elementoNumeroPaginaAtual.val());

        const paginaEhValidaParaVoltar = numeroPaginaAtual > 2;
        if (paginaEhValidaParaVoltar) {
            const proximaPagina = numeroPaginaAtual + 1;
            this.elementoNumeroPaginaAtual.val(proximaPagina);
        }
    }

    public instanciaSelectNumeroPaginaElemento() {
        this.elementoNumeroPaginaAtual = $("#numeroQuestaoPorPagina");
    }

    public instanciaQuestoesElemento() {
        this.elementoQuestoes = $(".container-questao");
    }

    public onLoad() {
        this.instanciaSelectNumeroPaginaElemento();
        this.instanciaQuestoesElemento();
        this.instanciaNumeroPaginaElemento();
    }
    public instanciaNumeroPaginaElemento() {
        this.elementoNumeroPaginaAtual = $("#pagina-atual");
    }
}

var controller = new IndexController();

console.log("chegou no arquivo Ts");