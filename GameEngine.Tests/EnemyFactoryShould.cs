using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameEngine.Tests
{
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        private readonly EnemyFactory _sut;

        public EnemyFactoryShould()
        {
            _sut = new EnemyFactory();
        }

        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            Enemy enemy = _sut.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact(Skip = "Don't need to run this test")]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            Enemy enemy = _sut.Create("Zombie");

            Assert.IsNotType<DateTime>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            Enemy enemy = _sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            Enemy enemy = _sut.Create("Zombie King", true);

            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            Assert.Equal("Zombie King", boss.Name);
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            EnemyFactory _sut = new EnemyFactory();

            Enemy enemy = _sut.Create("Zombie King", true);

            //Assert.IsType<Enemy>(enemy);
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeperateInstance()
        {
            Enemy enemy1 = _sut.Create("Zombie");
            Enemy enemy2 = _sut.Create("Zombie");

            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowedNullName()
        {
            //Assert.Throws<ArgumentNullException>(() => _sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => _sut.Create(null));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            EnemyCreationException ex = Assert.Throws<EnemyCreationException>(() => _sut.Create("Zombie", true));

            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }
    }
}
