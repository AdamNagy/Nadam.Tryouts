// See https://aka.ms/new-console-template for more information
using Multithreading;

var testDataGenerator = new TestDataGenerator(
    @"C:\Users\Adam_Nagy1\Documents",
    "test-urls.txt",
    "test-data");

testDataGenerator.GenerateTestFile();
