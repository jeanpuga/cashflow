﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace FuncionalTest.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Cenario0001_UploadEProcessamentoFeature : object, Xunit.IClassFixture<Cenario0001_UploadEProcessamentoFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Cenario0001-UploadEProcessamento.feature"
#line hidden
        
        public Cenario0001_UploadEProcessamentoFeature(Cenario0001_UploadEProcessamentoFeature.FixtureData fixtureData, FuncionalTest_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "Features", "Cenario0001-UploadEProcessamento", @"** Processo de Calculo - primeira versão
Na primeira versão a entrega garante a subida dio artefato ""planilha de calculo"".
- etapa 1
Upload da planilha
- etapa 2
Salvar em bucket S3
- etapa 3
Ler a planilha e extrair os dados
- etapa 4
Recebe os fatorese processa o calculo

*** A validação das funcionalidades da planilha acontece mediante a ultima etapa do processo que é o calculo.
Isso acontece porque na ""etapa 3"" retorna dois payloads?
- o q será enviado na etapa 4 - calculo
- e o resultado do calculo pela planilha do qual pode ser comparado", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Upload da planilha e processamento")]
        [Xunit.TraitAttribute("FeatureTitle", "Cenario0001-UploadEProcessamento")]
        [Xunit.TraitAttribute("Description", "Upload da planilha e processamento")]
        [Xunit.TraitAttribute("Category", "CalculateProcess")]
        public virtual void UploadDaPlanilhaEProcessamento()
        {
            string[] tagsOfScenario = new string[] {
                    "CalculateProcess"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Upload da planilha e processamento", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 22
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 23
 testRunner.Given("a planilha preenchida \"exemplo1.xlsx\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 24
 testRunner.When("realizo upload da planilha", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 25
 testRunner.And("depois executo o processo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "ManualTotal",
                            "ManualAvoided",
                            "AutomaticTotal",
                            "AutomaticAvoided"});
                table1.AddRow(new string[] {
                            "5.174277520532012",
                            "2.6447544683951905",
                            "5.142054355930715",
                            "2.6447543147978463"});
#line 26
 testRunner.Then("deve apresentar os valores calculados", ((string)(null)), table1, "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                Cenario0001_UploadEProcessamentoFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                Cenario0001_UploadEProcessamentoFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion