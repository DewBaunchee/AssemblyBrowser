using AssemblyBrowserLib.Entity.Member;
using AssemblyBrowserLib.Entity.Namespace;
using AssemblyBrowserLib.Entity.Type;
using NUnit.Framework;

namespace AssemblyBrowserTests
{
    public class Tests
    {

        private readonly string _propertyToStringResult = "PropType prop{ get; }"; 
        private readonly string _fieldToStringResult = "private FieldType field"; 
        private readonly string _methodToStringResult = "public MethodReturnType method(int anInt)"; 
        private readonly string _namespaceToStringResult = "Namespace: namespace"; 
        private readonly string _classToStringResult = "Class: abstract class"; 
        private readonly string _interfaceToStringResult = "Interface: interface"; 
        private readonly string _enumToStringResult = "Enum: enum"; 
        private readonly string _structToStringResult = "Struct: struct"; 
        private readonly string _recordToStringResult = "Record: record"; 
        
        [Test]
        public void NodesToStringTest()
        {
            Property property = new Property("prop")
            {
                Type = "PropType",
                CanRead = true,
                CanWrite = false
            };
            Assert.AreEqual(_propertyToStringResult, property.ToString());

            Field field = new Field("field")
            {
                Type = "FieldType",
                AccessModifier = "private"
            };
            Assert.AreEqual(_fieldToStringResult, field.ToString());

            Method method = new Method("method")
            {
                Args = new[] {"int anInt"},
                AccessModifier = "public",
                ReturnType = "MethodReturnType"
            };
            Assert.AreEqual(_methodToStringResult, method.ToString());
            
            Namespace aNamespace = new Namespace("namespace");
            Assert.AreEqual(_namespaceToStringResult, aNamespace.ToString());

            Class aClass = new Class("class", true);
            Assert.AreEqual(_classToStringResult, aClass.ToString());

            Interface anInterface = new Interface("interface");
            Assert.AreEqual(_interfaceToStringResult, anInterface.ToString());

            Enum anEnum = new Enum("enum");
            Assert.AreEqual(_enumToStringResult, anEnum.ToString());

            Struct aStruct = new Struct("struct");
            Assert.AreEqual(_structToStringResult, aStruct.ToString());

            Record aRecord = new Record("record");
            Assert.AreEqual(_recordToStringResult, aRecord.ToString());
        }
    }
}