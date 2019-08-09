namespace CMP.PipelinesAndSchemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://CMP.PO",@"PO")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"PO"})]
    public sealed class PO : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://CMP.PO"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://CMP.PO"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <schemaEditorExtension:schemaInfo namespaceAlias=""b"" extensionClass=""Microsoft.BizTalk.FlatFileExtension.FlatFileExtension"" standardName=""Flat File"" xmlns:schemaEditorExtension=""http://schemas.microsoft.com/BizTalk/2003/SchemaEditorExtensions"" />
      <b:schemaInfo standard=""Flat File"" codepage=""65001"" default_pad_char="" "" count_positions_by_byte=""false"" parser_optimization=""speed"" lookahead_depth=""3"" suppress_empty_nodes=""false"" generate_empty_nodes=""true"" allow_early_termination=""false"" allow_message_breakup_of_infix_root=""false"" compile_parse_tables=""false"" root_reference=""PO"" pad_char_type=""char"" early_terminate_optional_fields=""false"" />
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""PO"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo tag_name=""PO"" structure=""delimited"" child_delimiter_type=""hex"" child_delimiter=""0xD 0xA"" child_order=""postfix"" sequence_number=""1"" preserve_delimiter_for_empty_data=""true"" suppress_trailing_delimiters=""false"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:annotation>
          <xs:appinfo>
            <groupInfo sequence_number=""0"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
          </xs:appinfo>
        </xs:annotation>
        <xs:element name=""BillTo"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo structure=""positional"" sequence_number=""2"" preserve_delimiter_for_empty_data=""true"" suppress_trailing_delimiters=""false"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:annotation>
                <xs:appinfo>
                  <groupInfo sequence_number=""0"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
                </xs:appinfo>
              </xs:annotation>
              <xs:element name=""Country"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""10"" sequence_number=""1"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Name"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""20"" sequence_number=""2"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Street"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""20"" sequence_number=""3"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""City"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""15"" sequence_number=""4"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""State"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""3"" sequence_number=""5"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Zip"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""5"" sequence_number=""6"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""ShipTo"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo structure=""positional"" sequence_number=""3"" preserve_delimiter_for_empty_data=""true"" suppress_trailing_delimiters=""false"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:annotation>
                <xs:appinfo>
                  <groupInfo sequence_number=""0"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
                </xs:appinfo>
              </xs:annotation>
              <xs:element name=""Country"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""10"" sequence_number=""1"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Name"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""20"" sequence_number=""2"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Street"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""20"" sequence_number=""3"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""City"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""15"" sequence_number=""4"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""State"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""3"" sequence_number=""5"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
              <xs:element name=""Zip"" type=""xs:string"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:fieldInfo justification=""left"" pos_offset=""0"" pos_length=""5"" sequence_number=""6"" />
                  </xs:appinfo>
                </xs:annotation>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""Comment"" type=""xs:string"">
          <xs:annotation>
            <xs:appinfo>
              <b:fieldInfo justification=""left"" sequence_number=""4"" />
            </xs:appinfo>
          </xs:annotation>
        </xs:element>
        <xs:element name=""Items"">
          <xs:annotation>
            <xs:appinfo>
              <b:recordInfo tag_name=""ITEMS"" structure=""delimited"" child_delimiter_type=""char"" child_delimiter="","" child_order=""prefix"" sequence_number=""5"" preserve_delimiter_for_empty_data=""true"" suppress_trailing_delimiters=""false"" />
            </xs:appinfo>
          </xs:annotation>
          <xs:complexType>
            <xs:sequence>
              <xs:annotation>
                <xs:appinfo>
                  <groupInfo sequence_number=""0"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
                </xs:appinfo>
              </xs:annotation>
              <xs:element maxOccurs=""unbounded"" name=""Item"">
                <xs:annotation>
                  <xs:appinfo>
                    <b:recordInfo tag_name=""ITEM"" structure=""delimited"" child_delimiter_type=""char"" child_delimiter=""|"" child_order=""infix"" sequence_number=""1"" preserve_delimiter_for_empty_data=""true"" suppress_trailing_delimiters=""false"" />
                  </xs:appinfo>
                </xs:annotation>
                <xs:complexType>
                  <xs:sequence>
                    <xs:annotation>
                      <xs:appinfo>
                        <groupInfo sequence_number=""0"" xmlns=""http://schemas.microsoft.com/BizTalk/2003"" />
                      </xs:appinfo>
                    </xs:annotation>
                    <xs:element name=""ID"" type=""xs:string"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:fieldInfo justification=""left"" sequence_number=""1"" />
                        </xs:appinfo>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name=""Name"" type=""xs:string"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:fieldInfo justification=""left"" sequence_number=""2"" />
                        </xs:appinfo>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name=""Quantity"" type=""xs:unsignedInt"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:fieldInfo justification=""left"" sequence_number=""3"" />
                        </xs:appinfo>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name=""Price"" type=""xs:float"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:fieldInfo justification=""left"" sequence_number=""4"" />
                        </xs:appinfo>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name=""Comment"" type=""xs:string"">
                      <xs:annotation>
                        <xs:appinfo>
                          <b:fieldInfo justification=""left"" sequence_number=""5"" />
                        </xs:appinfo>
                      </xs:annotation>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
      <xs:attribute name=""Date"" type=""xs:date"">
        <xs:annotation>
          <xs:appinfo>
            <b:fieldInfo justification=""left"" sequence_number=""1"" />
          </xs:appinfo>
        </xs:annotation>
      </xs:attribute>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public PO() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "PO";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
