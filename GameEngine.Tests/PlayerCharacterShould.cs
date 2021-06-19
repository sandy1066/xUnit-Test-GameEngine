using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
        private readonly PlayerCharacter _sut;
        private readonly ITestOutputHelper _output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Creating New PlayerCharacter");

            _sut = new PlayerCharacter();
        }

        public void Dispose()
        {
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");

            //_sut.Dispose();
        }

        [Fact]
        public void BeInexperincedWhenNew()
        {
            // _sut : system under test

            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Equal("Sarah Smith", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameStartingWithFirstName()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.StartsWith("Sarah", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameEndingWithLastName()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.EndsWith("Smith", _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnorecaseAssertExample()
        {
            _sut.FirstName = "SARAH";
            _sut.LastName = "SMITH";

            Assert.Equal("Sarah Smith", _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CalculateFullName_SubstringAssertExample()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Contains("ah Sm", _sut.FullName);
        }

        [Fact]
        public void CalculateFullNameWithTitleCase()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]", _sut.FullName);
        }

        [Fact]
        public void StartsWithDefaultHealth()
        {
            Assert.Equal(100, _sut.Health);
        }

        [Fact]
        public void StartsWithDeafaultHealth_NotEqualExample()
        {
            Assert.NotEqual(101, _sut.Health);
        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            _sut.Sleep();

            //Assert.True(_sut.Health >= 101 && _sut.Health <= 200);
            Assert.InRange(_sut.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            Assert.Null(_sut.Nickname);
        }

        [Fact]
        public void WeaponHavingALongBow()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
        }

        [Fact]
        public void WeaponNotHavingAStaffOfWonder()
        {
            Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
        }

        [Fact]
        public void WeaponHavingAtleastOneKindOfSword()
        {
            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var ExpectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(ExpectedWeapons, _sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons()
        {
            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void RaiseSleptEvent()
        {
            Assert.Raises<EventArgs>(handler => _sut.PlayerSlept += handler, handler => _sut.PlayerSlept -= handler, () => _sut.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));
        }

        [Theory]
        //[MemberData(nameof(InternalHealthDamageTestData.TestData), MemberType = typeof(InternalHealthDamageTestData))]
        //[MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]
        //[HealthDamageData]
        [HealthDamageDataAttributeUsingExternalCSV]
        public void TakeDamage(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, _sut.Health);
        }
    }
}
