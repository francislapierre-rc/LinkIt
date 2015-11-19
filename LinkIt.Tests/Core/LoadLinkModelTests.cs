﻿using System.Collections.Generic;
using System.Linq;
using ApprovalTests.Reporters;
using LinkIt.ConfigBuilders;
using LinkIt.PublicApi;
using LinkIt.Tests.Shared;
using NUnit.Framework;

namespace LinkIt.Tests.Core {
    [UseReporter(typeof(DiffReporter))]
    [TestFixture]
    public class LoadLinkModelTests
    {
        private ILoadLinkProtocol _sut;

        [SetUp]
        public void SetUp() {
            var loadLinkProtocolBuilder = new LoadLinkProtocolBuilder();
            loadLinkProtocolBuilder.For<SingleReferenceLinkedSource>()
                .LoadLinkReferenceById(
                    linkedSource => linkedSource.Model.SummaryImageId,
                    linkedSource => linkedSource.SummaryImage
                );
            _sut = loadLinkProtocolBuilder.Build(()=>new ReferenceLoaderStub());
        }

        [Test]
        public void LoadLink_WithModel_ShouldLinkModel() {
            var actual = _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModel(
                    new SingleReferenceContent {
                        Id = "1",
                        SummaryImageId = "a"
                    }
                );

            Assert.That(actual.Model.Id, Is.EqualTo("1"));
            Assert.That(actual.SummaryImage.Id, Is.EqualTo("a"));
        }

        [Test]
        public void LoadLink_WithWrontModelType_ShouldThrow() {
            TestDelegate act = () => _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModel(
                    "The model of SingleReferenceLinkedSource is not a string"
                );

            Assert.That(act, Throws.ArgumentException
                .With.Message.ContainsSubstring("SingleReferenceContent").And
                .With.Message.ContainsSubstring("String")
            );
        }

        [Test]
        public void LoadLink_ModelWithNullModel_ShouldReturnNull() {
            var actual = _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModel<SingleReferenceContent>(null);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void LoadLink_WithModels_ShouldLinkModels() {
            var actual = _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModels(
                    new SingleReferenceContent {
                        Id = "1",
                        SummaryImageId = "a"
                    },
                    new SingleReferenceContent {
                        Id = "2",
                        SummaryImageId = "b"
                    }
                );

            var summaryImageIds = actual
                .Select(item => item.SummaryImage.Id)
                .ToList();
            Assert.That(summaryImageIds, Is.EquivalentTo(new[] {"a", "b"}));
        }

        [Test]
        public void LoadLink_ModelsWithNullModel_ShouldLinkNull() {
            var actual = _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModels<SingleReferenceContent>(null);
            
            Assert.That(actual, Is.EquivalentTo(new List<SingleReferenceLinkedSource>{null}));
        }

        [Test]
        public void LoadLink_ModelsWithWrongModelType_ShouldThrow() {
            TestDelegate act = () => _sut.LoadLink<SingleReferenceLinkedSource>()
                .FromModels(
                    "The model of SingleReferenceLinkedSource is not a string"
                );

            Assert.That(act, Throws.ArgumentException
                .With.Message.ContainsSubstring("SingleReferenceContent").And
                .With.Message.ContainsSubstring("String")
            );
        }
    }
}