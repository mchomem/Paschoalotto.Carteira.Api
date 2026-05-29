namespace Paschoalotto.Carteira.Core.Application.Services;

public class DocumentoService : IDocumentoService
{
    public void ValidarDocumento(string documento)
    {
        var documentoSemMascara = documento
                                    .Replace(".", string.Empty)
                                    .Replace("-", string.Empty)
                                    .Replace("/", string.Empty);

        if (documentoSemMascara.Length == 11)
        {
            ValidarCPF(documentoSemMascara);
        }
        else if(documentoSemMascara.Length == 14)
        {
            ValidarCNPJ(documentoSemMascara);
        }
        else
        {
            throw new DocumentoInvalidoException($"Documento inválido. Documento informado: {documento}");
        }
    }

    private void ValidarCPF(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            throw new CPFInvalidException("CPF não pode ser nulo ou vazio.");

        // Remove caracteres não numéricos
        var cpf = new string(documento.Where(char.IsDigit).ToArray());

        // Verifica se tem 11 dígitos
        if (cpf.Length != 11)
            throw new CPFInvalidException($"CPF deve conter 11 dígitos. Documento informado: {documento}");

        // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
        if (cpf.Distinct().Count() == 1)
            throw new CPFInvalidException($"CPF inválido. Todos os dígitos são iguais. Documento informado: {documento}");

        // Calcula o primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
            throw new CPFInvalidException($"CPF inválido. Primeiro dígito verificador incorreto. Documento informado: {documento}");

        // Calcula o segundo dígito verificador
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[10].ToString()) != digitoVerificador2)
            throw new CPFInvalidException($"CPF inválido. Segundo dígito verificador incorreto. Documento informado: {documento}");
    }

    private void ValidarCNPJ(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            throw new CNPJInvalidException("CNPJ não pode ser nulo ou vazio.");

        // Remove caracteres não numéricos
        var cnpj = new string(documento.Where(char.IsDigit).ToArray());

        // Verifica se tem 14 dígitos
        if (cnpj.Length != 14)
            throw new CNPJInvalidException($"CNPJ deve conter 14 dígitos. Documento informado: {documento}");

        // Verifica se todos os dígitos são iguais (ex: 11.111.111/1111-11)
        if (cnpj.Distinct().Count() == 1)
            throw new CNPJInvalidException($"CNPJ inválido. Todos os dígitos são iguais. Documento informado: {documento}");

        // Calcula o primeiro dígito verificador
        int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
            throw new CNPJInvalidException($"CNPJ inválido. Primeiro dígito verificador incorreto. Documento informado: {documento}");

        // Calcula o segundo dígito verificador
        int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cnpj[13].ToString()) != digitoVerificador2)
            throw new CNPJInvalidException($"CNPJ inválido. Segundo dígito verificador incorreto. Documento informado: {documento}");
    }
}
