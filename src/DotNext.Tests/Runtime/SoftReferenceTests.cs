using System.Runtime.CompilerServices;

namespace DotNext.Runtime
{
    public sealed class SoftReferenceTests : Test
    {
        private sealed class Target
        {
            internal bool IsAlive = true;

            ~Target() => IsAlive = false;
        }

        [Fact]
        public static void EscapeGen0()
        {
            var reference = CreateReference();

            for (var i = 0; i < 100; i++)
            {
                new object();
                GC.Collect(generation: 0);
                True(IsAlive(reference));
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static SoftReference<Target> CreateReference() => new(new());

            [MethodImpl(MethodImplOptions.NoInlining)]
            static bool IsAlive(SoftReference<Target> r) => r.TryGetTarget(out _);
        }

        [Fact]
        public static void WithOptions()
        {
            var reference = CreateReference();

            for (var i = 0; i < 100; i++)
            {
                new object();
                GC.Collect();
            }

            Null((Target)reference);

            [MethodImpl(MethodImplOptions.NoInlining)]
            static SoftReference<Target> CreateReference()
                => new(new(), new SoftReferenceOptions { CollectionCount = int.MaxValue, MemoryLimit = 1 });
        }

        [Fact]
        public static void Operators()
        {
            var target = new object();
            var ref1 = new SoftReference<object>(target);
            var ref2 = ref1;

            Equal(ref1, ref2);
            True(ref1 == ref2);
            False(ref1 != ref2);
            Equal(ref1, target);
            Equal(ref1.GetHashCode(), ref2.GetHashCode());

            ref2 = default;
            NotEqual(ref1, ref2);
            False(ref1 == ref2);
            True(ref1 != ref2);
            NotEqual(target, ref1);
            NotEqual(ref1.GetHashCode(), ref2.GetHashCode());
        }
    }
}