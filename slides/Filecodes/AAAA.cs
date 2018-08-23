[PexMethod]
void AssumeActAssert(ArrayList list, object item) { 
    // assume
    PexAssume.IsNotNull(list);
    // arrange
    var count = list.Count;
    // act
    list.Add(item);
    // assert
    Assert.IsTrue(list.Count == count + 1);
}