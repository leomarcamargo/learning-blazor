function provaPontoNetStatic() {
    DotNet.invokeMethodAsync("BlazorPeliculas.Client", "ObterCurrentCount")
        .then(resultado => {
            console.log("Dentro do JS " + resultado);
        });
}

function provaPontoNetInstancia(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}