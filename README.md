# FolderSort — Parallel Performance Experiment

A C# console application created as a university assignment to investigate the impact of parallelism on CPU-bound workloads.

The application compares sequential and parallel processing of datasets using Bubble Sort as an O(n²) operation and measures the resulting speedup.

## Project Structure

```
FolderSort/
├── Core/
│   ├── FileGenerator.cs       # Generates 3 × 120 test files
│   ├── BubbleSorter.cs        # Bubble Sort implementation (O(n²))
│   ├── FileProcessor.cs       # Reads a file and processes its contents
│   ├── SequentialProcessor.cs # Sequential baseline execution
│   ├── ParallelProcessor.cs   # Parallel processing using worker threads
│   ├── BenchmarkRunner.cs     # Runs experiments and exports results
│   └── BenchmarkResult.cs     # Benchmark result model
├── Data/                      # Generated datasets
│   ├── Small/                 # 120 files × 500 integers
│   ├── Medium/                # 120 files × 3000 integers
│   └── Large/                 # 120 files × 15000 integers
├── Results/                   # Generated benchmark CSV files
├── Program.cs
└── README.md
```

> Note: The `Data/` directory is generated automatically on the first application launch and is not required to be stored in the repository.

## Experiment Overview

Three independent datasets containing 120 files each are generated automatically. The datasets differ only in file size:

| Dataset | Files | Integers per File |
| ------- | ----- | ----------------- |
| Small   | 120   | 500               |
| Medium  | 120   | 3000              |
| Large   | 120   | 15000             |

Each file contains randomly generated unsorted integers.

The same operation is performed on every file:

* Read integers from the file,
* Sort them using Bubble Sort,
* Measure total execution time.

Bubble Sort was intentionally selected because its O(n²) complexity provides a sufficiently heavy computational workload for benchmarking.

## Parallel Processing Strategy

The experiment measures execution time using different numbers of worker threads.

The sequential version processes all files one by one.

The parallel version distributes work dynamically between worker threads using a shared task queue, ensuring that threads that finish earlier continue processing remaining files instead of waiting idly.

This approach reduces workload imbalance and improves resource utilisation.

## Running the Project

### Requirements

* .NET 8 SDK (or compatible version)

### Run

```bash
dotnet run
```

On startup, the application will:

1. Generate datasets if they do not already exist,
2. Execute sequential benchmarks,
3. Execute parallel benchmarks using different worker counts,
4. Export benchmark results to CSV files.

Generated results are saved in the `Results/` directory.

## Speedup Formula

Speedup is calculated as:

Sp = T1 / Tp

where:

* T1 — execution time of the sequential version,
* Tp — execution time using p worker threads.

## Example Results

| Workers | Small (ms) | Medium (ms) | Large (ms) |
| ------- | ---------: | ----------: | ---------: |
| 1       |         97 |        2789 |      84146 |
| 2       |         40 |        1495 |      57587 |
| 4       |         30 |         977 |      35741 |
| 8       |         22 |         708 |      23192 |

## Observations

* Larger datasets benefit more from parallel execution.
* Small datasets quickly reach diminishing returns because thread management overhead becomes significant.
* Speedup is not perfectly linear.
* Increasing the number of workers beyond a certain point provides limited improvement due to I/O costs and parallelisation overhead.

## Educational Objectives

This project demonstrates:

* implementation of an O(n²) algorithm,
* file generation and processing,
* sequential versus parallel execution,
* dynamic workload distribution,
* execution time measurement,
* benchmark data collection,
* performance analysis and interpretation of results.
