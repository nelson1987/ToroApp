using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToroApp.Domain.Tests
{
    public class TransferenciaTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RealizarTransferenciaPix()
        {
            Beneficiario beneficiario = new Beneficiario("352", "0001", "300123");
            Remetente remetente = new Remetente("033", "03312", "45358996060");
            TransferenciaService transferencia = new TransferenciaService(beneficiario, remetente, 1000.00).Realizar();
            if (transferencia.IsValid())
                transferencia.Realizar();
            Assert.Pass();
        }
    }
    public class Conta
    {
        public Conta(string cpf, string numeroConta)
        {
            Cpf = cpf;
            NumeroConta = numeroConta;
        }

        public string Cpf { get; private set; }
        public string NumeroConta { get; private set; }
        public List<Movimentacao> Movimentacoes { get; private set; }
    }

    public class ContaRepository
    {
        public Conta BuscarConta(Beneficiario beneficiario)
        {
            List<Conta> contas = new List<Conta>();
            contas.Add(new Conta("45358996060", "300123"));

            return contas.FirstOrDefault(x => x.NumeroConta == beneficiario.NumeroConta);
        }

        internal void IncluirMovimentacao(Movimentacao movimentacao)
        {
            throw new NotImplementedException();
        }
    }

    public class TransferenciaService
    {
        private ContaRepository _repository { get; set; }
        private Movimentacao movimentacao { get; set; }
        public TransferenciaService(Beneficiario beneficiario, Remetente remetente, double valor)
        {
            Beneficiario = beneficiario;
            Remetente = remetente;
            Valor = valor;
        }

        public Beneficiario Beneficiario { get; }
        public Remetente Remetente { get; }
        public double Valor { get; }

        public TransferenciaService IsValid()
        {
            var conta = _repository.BuscarConta(Beneficiario);
            if (Remetente.Cpf != conta.Cpf)
                throw new Exception("O cpf não é igual ao do usuário beneficiário");
            return this;
        }

        internal void Realizar()
        {
            var conta = _repository.BuscarConta(Beneficiario);
            conta.IncluirMovimentacao(movimentacao);
            _repository.Alterar();
        }
    }
    public class Movimentacao
    {
        public string NumeroConta { get; private set; }
        public string BancoRemetente { get; private set; }
        public string AgenciaRemetente { get; private set; }
        public string CpfRemetente { get; private set; }
        public double Valor { get; }
    }

    public class Beneficiario
    {
        public Beneficiario(string banco, string agencia, string conta)
        {
            Banco = banco;
            Agencia = agencia;
            NumeroConta = conta;
        }

        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string NumeroConta { get; private set; }
    }

    public class Remetente
    {
        public Remetente(string banco, string agencia, string cpf)
        {
            Banco = banco;
            Agencia = agencia;
            Cpf = cpf;
        }

        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string Cpf { get; private set; }
    }
}
