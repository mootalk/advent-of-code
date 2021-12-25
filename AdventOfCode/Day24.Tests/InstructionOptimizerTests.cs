using System.Collections.Generic;
using System.Linq;
using Day24.Arguments;
using Day24.Instructions;
using Xunit;

namespace Day24.Tests
{
    public class InstructionOptimizerTests
    {
        private readonly InputReader _inputReader = new();
        private readonly Variable _w = new('w');
        private readonly Variable _x = new('x');

        [Fact]
        public void Divide_by_1_instruction_is_removed()
        {
            var optimized = Optimize(
                new DivideInstruction(_w, 1));

            Assert.Empty(optimized);
        }

        [Fact]
        public void Multiply_by_0_resets_the_variable()
        {
            var optimized = Optimize(
                    new InputInstruction(_w, _inputReader),
                    new MultiplierInstruction(_w, 0));

            var resetInstruction = optimized[1];
            Assert.IsType<ResetInstruction>(resetInstruction);
            Assert.Equal(_w, resetInstruction.Variable);
        }

        [Fact]
        public void Reset_followed_by_add_is_combined_into_set_for_same_variable()
        {
            var optimized = Optimize(
                    new ResetInstruction(_w),
                    new AddInstruction(_w, 1));

            var instruction = optimized.Single();
            Assert.IsType<SetInstruction>(instruction);
            Assert.Equal(_w, instruction.Variable);

            var setInstruction = (SetInstruction)instruction;
            Assert.Equal(
                new List<Argument> { 1 },
                setInstruction.Arguments);

            optimized = Optimize(
                new ResetInstruction(_w),
                new AddInstruction(_x, 1));

            Assert.Equal(2, optimized.Count);
        }

        [Fact]
        public void Set_and_add_are_combined_for_same_variable()
        {
            var optimized = Optimize(
                    new SetInstruction(_w, 3),
                    new AddInstruction(_w, 1));

            var instruction = optimized.Single();
            Assert.IsType<SetInstruction>(instruction);
            Assert.Equal(_w, instruction.Variable);

            var setInstruction = (SetInstruction)instruction;
            Assert.Equal(
                new List<Argument> { 4 },
                setInstruction.Arguments);

            optimized = Optimize(
                    new SetInstruction(_w, 3),
                    new AddInstruction(_x, 1));
            Assert.Equal(2, optimized.Count);
        }

        [Fact]
        public void Equals_can_be_inverted_for_same_variable()
        {
            var optimized = Optimize(
                new EqualsInstruction(_w, 23),
                new EqualsInstruction(_w, 0));

            var instruction = optimized.Single();
            Assert.IsType<NotEqualsInstruction>(instruction);
            Assert.Equal(_w, instruction.Variable);

            var notEqualsInstruction = (NotEqualsInstruction)instruction;
            Assert.Equal(23, notEqualsInstruction.Argument.Value);

            optimized = Optimize(
                new EqualsInstruction(_w, 23),
                new EqualsInstruction(_x, 0));
            Assert.Equal(2, optimized.Count);
        }

        [Fact]
        public void Multiply_for_unitinitialized_variable_is_removed()
        {
            var optimized = Optimize(
                new MultiplierInstruction(_w, 2));

            Assert.Empty(optimized);
        }

        [Fact]
        public void Divide_for_unitinitialized_variable_is_removed()
        {
            var optimized = Optimize(
                new DivideInstruction(_w, 2));

            Assert.Empty(optimized);
        }

        [Fact]
        public void Modulo_for_unitinitialized_variable_is_removed()
        {
            var optimized = Optimize(
                new ModuloInstruction(_w, 2));

            Assert.Empty(optimized);
        }

        [Fact]
        public void Add_for_uninitialized_variable_argument_is_removed()
        {
            var optimized = Optimize(
                new InputInstruction(_w, _inputReader),
                new AddInstruction(_w, _x));

            Assert.Single(optimized);
            Assert.IsType<InputInstruction>(optimized[0]);
        }

        private static List<Instruction> Optimize(params Instruction[] instructions)
            => InstructionOptimizer.OptimizeInstructions(instructions.ToList());
    }
}
