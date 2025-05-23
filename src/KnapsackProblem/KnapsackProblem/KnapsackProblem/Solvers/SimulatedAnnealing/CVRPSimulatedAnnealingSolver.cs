﻿using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.SimulatedAnnealing
{
    // https://en.wikipedia.org/wiki/Simulated_annealing
    // https://www.geeksforgeeks.org/simulated-annealing/
    public class CVRPSimulatedAnnealingSolver : ISolver<BestCVRPData>
    {
        private delegate double ChangeTemperature(double currentTemperature);

        private CVRProblem _problem;
        private BestCVRPDataSimulatedAnnealing _bestCVRPData;
        private SimulatedAnnealingGenericData _algorithmData;
        private int _evaluationCount;
        private bool _isUsingEvaluations = false;
        private ChangeTemperature _temperatureChangeStrategy;
        private int _countOfCalls;

        public CVRPSimulatedAnnealingSolver(CVRProblem problem, SimulatedAnnealingGenericData data, bool isUsingEvaluations, TemperatureChangeType temperatureChangeType)
        {
            _problem = problem;
            _bestCVRPData = new BestCVRPDataSimulatedAnnealing(problem.CitiesCount);
            _algorithmData = data;
            _evaluationCount = 0;
            _countOfCalls = 0;
            _isUsingEvaluations = isUsingEvaluations;

            _temperatureChangeStrategy = (temperatureChangeType) switch
            {
                TemperatureChangeType.Linear => LinearTemperatureChange,
                TemperatureChangeType.Exponential => ExponentialTemperatureChange,
                TemperatureChangeType.Logarithmic => LogarithmicTemperatureChange,
                _ => throw new NotImplementedException()
            };
        }

        public BestCVRPData FindOptimalSolution()
        {
            _bestCVRPData.Clear();

            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = Max Value.");
                return _bestCVRPData;
            }
            _evaluationCount = 0;
            _countOfCalls = 0;

            // energy goal function
            // neighbour generator
            // neighbour is a solution that is near, swapped two vertices etc.

            // annealing schedule - temperature 
            // initial temperature 
            // minimal temperature
            // 
            // acceptance probability

            // implement base solution s
            var solution = new int[_problem.CitiesCount];

            var rng = new Random();
            for (int i = 0; i < solution.Length; i++)
            {
                solution[i] = i + 1;
            }
            rng.Shuffle(solution);
            var initEval = _problem.CalculateFitness(solution);
            _evaluationCount++;

            _bestCVRPData.UpdateBestCVRPData(0, initEval, solution);

            // initial temperature is mostly set at 1 and minimal temperature is a 10^-n
            var currentTemp = _algorithmData.InitialTemperature;
            var minTemp = _algorithmData.MinimalTemperature;


            // to keep the solution fitness from recalculation
            var solutionChanged = false;
            var solutionFitness = _problem.CalculateFitness(solution);
            _evaluationCount++;

            if (_isUsingEvaluations)
            {
                while (currentTemp > minTemp)
                {
                    for (int i = 0; i < _algorithmData.IterationsPerCoolingPeriod; i++)
                    {
                        // find neighbour
                        var neighbour = CreateNeighbour(solution, NeighbourCreationAlgorithm.SwapRandomElements);

                        if (solutionChanged)
                        {
                            solutionFitness = _problem.CalculateFitness(solution);
                            _evaluationCount++;
                            solutionChanged = false;
                        }
                        var neighbourFitness = _problem.CalculateFitness(neighbour);
                        _evaluationCount++;

                        if (_bestCVRPData.Fitness > neighbourFitness)
                        {
                            _bestCVRPData.UpdateBestCVRPData(i, neighbourFitness, neighbour, currentTemp);
                        }

                        double probability = Math.Pow(Math.E, (solutionFitness - neighbourFitness) / currentTemp);

                        if (probability > rng.NextDouble())
                        {
                            Buffer.BlockCopy(neighbour, 0, solution, 0, solution.Length * sizeof(int));
                            //Array.Copy(neighbour, solution, neighbour.Length);
                            solutionChanged = true;
                        }
                        _countOfCalls++;
                        if (_evaluationCount > _algorithmData.MaxFitnessComparisonCount)
                        {
                            break;
                        }
                    }
                    // decreasing by multiplication (Alpha < 1 && Alpha > 0)
                    currentTemp *= _algorithmData.Alpha;
                }
            }
            else
            {
                while (currentTemp > minTemp)
                {
                    for (int i = 0; i < _algorithmData.IterationsPerCoolingPeriod; i++)
                    {
                        // find neighbour
                        var neighbour = CreateNeighbour(solution, NeighbourCreationAlgorithm.SwapRandomElements);

                        if (solutionChanged)
                        {
                            solutionFitness = _problem.CalculateFitness(solution);
                            _evaluationCount++;
                            solutionChanged = false;
                        }
                        var neighbourFitness = _problem.CalculateFitness(neighbour);
                        _evaluationCount++;

                        if (_bestCVRPData.Fitness > neighbourFitness)
                        {
                            _bestCVRPData.UpdateBestCVRPData(i, neighbourFitness, neighbour, currentTemp);
                        }

                        double probability = Math.Pow(Math.E, (solutionFitness - neighbourFitness) / currentTemp);

                        if (probability > rng.NextDouble())
                        {
                            Buffer.BlockCopy(neighbour, 0, solution, 0, solution.Length * sizeof(int));
                            //Array.Copy(neighbour, solution, neighbour.Length);
                            solutionChanged = true;
                        }
                        _countOfCalls++;
                    }
                    // decreasing by multiplication (Alpha < 1 && Alpha > 0)
                    currentTemp = _temperatureChangeStrategy.Invoke(currentTemp);
                    //Console.WriteLine($"Current temperature: {currentTemp}.");
                }
            }

            //_bestCVRPData.DisplayBestData("Simulated Annealing");
            Console.WriteLine($"SA iterations sum through temperature changes {_countOfCalls}");
            Console.WriteLine($"Fitness evaluated {_evaluationCount} times.");
            return _bestCVRPData.Clone();
        }

        private int[] CreateNeighbour(int[] solution, NeighbourCreationAlgorithm creationAlgorithm)
        {
            var neighbour = new int[solution.Length];
            Array.Copy(solution, neighbour, solution.Length);

            switch (creationAlgorithm)
            {
                case (NeighbourCreationAlgorithm.MoveAllPointsLeftOrRight):
                    return ChangeNeigbourMoveAllPoints(neighbour);
                case (NeighbourCreationAlgorithm.SwapRandomElements):
                    return ChangeNeighbourSwapRandomElements(neighbour);
            }

            return neighbour;
        }

        private int[] ChangeNeigbourMoveAllPoints(int[] neighbour)
        {
            var rng = new Random();

            if (rng.Next(2) % 2 == 0)
            {
                // move left
                int zeroPositionElement = neighbour[0];
                for (int i = 1; i < neighbour.Length; i++)
                {
                    neighbour[i - 1] = neighbour[i];
                }
                neighbour[neighbour.Length - 1] = zeroPositionElement;
                return neighbour;
            }

            int lastPositionElement = neighbour[neighbour.Length - 1];
            for (int i = neighbour.Length - 2; i > -1; i--)
            {
                neighbour[i + 1] = neighbour[i];
            }
            neighbour[0] = lastPositionElement;
            return neighbour;
        }

        private int[] ChangeNeighbourSwapRandomElements(int[] neighbour)
        {
            var rng = new Random();

            var positionOne = rng.Next(0, neighbour.Length);
            var positionTwo = rng.Next(0, neighbour.Length);

            var aux = neighbour[positionOne];
            neighbour[positionOne] = neighbour[positionTwo];
            neighbour[positionTwo] = aux;

            return neighbour;
        }

        private double LinearTemperatureChange(double currentTemp)
        {
            return currentTemp - (_algorithmData.Alpha * _algorithmData.InitialTemperature);
        }

        private double ExponentialTemperatureChange(double currentTemp)
        {
            return currentTemp * _algorithmData.Alpha;
        }

        private double LogarithmicTemperatureChange(double currentTemp)
        {
            return _algorithmData.InitialTemperature / Math.Log(_countOfCalls + 1);
        }

        private enum NeighbourCreationAlgorithm
        {
            MoveAllPointsLeftOrRight,
            SwapRandomElements
        }

        public enum TemperatureChangeType
        {
            Linear,
            Exponential,
            Logarithmic
        }
    }
}
