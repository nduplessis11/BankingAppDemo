using SharedKernel.Application.Utilities;

namespace SharedKernel.Tests;

public class ResultTests
{
            [Fact]
        public void Success_ShouldReturnSuccessResult()
        {
            // Arrange
            const int value = 42;

            // Act
            var result = Result<int, string>.Success(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(42, result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Failure_ShouldReturnFailureResult()
        {
            // Arrange
            const string error = "Something went wrong";

            // Act
            var result = Result<int, string>.Failure(error);

            // Assert
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.Equal("Something went wrong", result.Error);
            Assert.Equal(default, result.Value);
        }

        [Fact]
        public void Map_ShouldTransformSuccessValue()
        {
            // Arrange
            var result = Result<int, string>.Success(42);

            // Act
            var mappedResult = result.Map(x => x * 2);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal(84, mappedResult.Value);
        }

        [Fact]
        public void Map_ShouldNotTransformFailureValue()
        {
            // Arrange
            var result = Result<int, string>.Failure("Error");

            // Act
            var mappedResult = result.Map(x => x * 2);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal("Error", mappedResult.Error);
        }

        [Fact]
        public void MapError_ShouldTransformFailureValue()
        {
            // Arrange
            var result = Result<int, string>.Failure("Error");

            // Act
            var mappedResult = result.MapError(err => $"Mapped {err}");

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal("Mapped Error", mappedResult.Error);
        }

        [Fact]
        public void Bind_ShouldChainSuccessResults()
        {
            // Arrange
            var result = Result<int, string>.Success(42);

            // Act
            var boundResult = result.Bind(x => Result<int, string>.Success(x * 2));

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(84, boundResult.Value);
        }

        [Fact]
        public void Bind_ShouldNotChainOnFailure()
        {
            // Arrange
            var result = Result<int, string>.Failure("Error");

            // Act
            var boundResult = result.Bind(x => Result<int, string>.Success(x * 2));

            // Assert
            Assert.True(boundResult.IsFailure);
            Assert.Equal("Error", boundResult.Error);
        }

        [Fact]
        public void OnSuccess_ShouldExecuteActionForSuccess()
        {
            // Arrange
            var result = Result<int, string>.Success(42);
            var actionExecuted = false;

            // Act
            result.OnSuccess(x =>
            {
                actionExecuted = true;
                Assert.Equal(42, x);
            });

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void OnFailure_ShouldExecuteActionForFailure()
        {
            // Arrange
            var result = Result<int, string>.Failure("Error");
            var actionExecuted = false;

            // Act
            result.OnFailure(err =>
            {
                actionExecuted = true;
                Assert.Equal("Error", err);
            });

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void Match_ShouldReturnSuccessValue()
        {
            // Arrange
            var result = Result<int, string>.Success(42);

            // Act
            var output = result.Match(
                success => $"Success: {success}",
                failure => $"Failure: {failure}");

            // Assert
            Assert.Equal("Success: 42", output);
        }

        [Fact]
        public void Match_ShouldReturnFailureValue()
        {
            // Arrange
            var result = Result<int, string>.Failure("Error");

            // Act
            var output = result.Match(
                success => $"Success: {success}",
                failure => $"Failure: {failure}");

            // Assert
            Assert.Equal("Failure: Error", output);
        }

        [Fact]
        public void Success_ShouldThrowArgumentNullException_WhenNullValue()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Result<string, string>.Success(null!));
        }

        [Fact]
        public void Failure_ShouldThrowArgumentNullException_WhenNullError()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Result<int, string>.Failure(null!));
        }
}