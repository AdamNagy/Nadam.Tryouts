using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DevGuideToCollections
{
    static class FilterParser
    {
        [Flags()]
        internal enum ComparisonOperator
        {
            None = 0,
            EqualTo = 1,
            LessThan = 2,
            GreaterThan = 4,
            LessThanEqualTo = EqualTo | LessThan,
            GreaterThanEqualTo = EqualTo | GreaterThan,
            NotEqualTo = LessThan | GreaterThan,
        }

        [Flags()]
        internal enum LogicalOperator
        {
            None = 0,
            And = 1,
            Or = 2,
            Not = 4,
        }

        internal class FilterNode
        {
            public virtual bool Eval(object item)
            {
                return true;
            }
        }

        internal class LogicalFilterNode : FilterNode
        {
            public FilterNode Left { get; set; }
            public LogicalOperator LogicalOperator { get; set; }
            public FilterNode Right { get; set; }

            public override bool Eval(object item)
            {
                switch (LogicalOperator)
                {
                    case LogicalOperator.And:
                        return Left.Eval(item) && Right.Eval(item);
                    case LogicalOperator.Or:
                        return Left.Eval(item) || Right.Eval(item);
                    case LogicalOperator.Not:
                        return !Right.Eval(item);
                }
                return base.Eval(item);
            }

            public override string ToString()
            {
                switch (LogicalOperator)
                {
                    case LogicalOperator.And:
                        return string.Format("{0} && {1}", Left, Right);
                    case LogicalOperator.Or:
                        return string.Format("{0} || {1}", Left, Right);
                    case LogicalOperator.Not:
                        return string.Format("!{0}", Right);
                }
                return base.ToString();
            }
        }

        internal class ExpressionFilterNode : FilterNode
        {
            public PropertyDescriptor PropertyDescriptor { get; set; }
            public string Property { get; set; }
            public string Value { get; set; }
            public object ConvertedValue { get; set; }
            public ComparisonOperator ComparisonOperator { get; set; }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                if ((ComparisonOperator & ComparisonOperator.GreaterThan) == ComparisonOperator.GreaterThan)
                {
                    sb.Append('>');
                }
                if ((ComparisonOperator & ComparisonOperator.LessThan) == ComparisonOperator.LessThan)
                {
                    sb.Append('<');
                }
                if ((ComparisonOperator & ComparisonOperator.EqualTo) == ComparisonOperator.EqualTo)
                {
                    sb.Append('=');
                }
                if (ComparisonOperator == ComparisonOperator.EqualTo)
                {
                    sb.Append('=');
                }

                if (sb.Length <= 0)
                {
                    return "[" + Property + "]";
                }

                return string.Format("{0} {1} '{2}'", Property, sb, Value);
            }

            public override bool Eval(object item)
            {
                if (item == null)
                {
                    return false;
                }

                if (PropertyDescriptor == null)
                {
                    var properties = TypeDescriptor.GetProperties(item.GetType());
                    PropertyDescriptor = properties.Find(Property, true);

                    if (PropertyDescriptor == null)
                    {
                        throw new ArgumentException(string.Format("Cannot find property '{0}' in type '{1}'", Property, item.GetType().FullName));
                    }
                }

                object value = PropertyDescriptor.GetValue(item);

                if (ComparisonOperator == ComparisonOperator.None)
                {
                    return Convert.ToBoolean(value);
                }

                if (ConvertedValue == null)
                {
                    if (PropertyDescriptor.Converter.CanConvertFrom(typeof(string)))
                    {
                        ConvertedValue = PropertyDescriptor.Converter.ConvertFromString(Value);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Cannot convert '{0}' to type '{1}'", Value, PropertyDescriptor.PropertyType.FullName));
                    }
                }

                int result = System.Collections.Comparer.Default.Compare(value, ConvertedValue);
                ComparisonOperator comparisonResult = ComparisonOperator.None;

                switch (result)
                {
                    case -1:
                        comparisonResult = ComparisonOperator.LessThan;
                        break;
                    case 0:
                        comparisonResult = ComparisonOperator.EqualTo;
                        break;
                    case 1:
                        comparisonResult = ComparisonOperator.GreaterThan;
                        break;
                }

                return ((ComparisonOperator & comparisonResult) == comparisonResult) ;
            }
        }

        static ComparisonOperator ParseComparison(char ch)
        {
            switch (ch)
            {
                case '=':
                    return ComparisonOperator.EqualTo;
                case '>':
                    return ComparisonOperator.GreaterThan;
                case '<':
                    return ComparisonOperator.LessThan;
            }
            return ComparisonOperator.None;
        }

        [Flags()]
        enum LookingFor
        {
            PropertyName = 1,
            ComparisonOperator = 2,
            Value = 4,
            LogicalOperator = 8,
        }
        
        static internal FilterNode Parse(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return null;
            }

            LookingFor lookingFor = LookingFor.PropertyName;
            bool inBracket = false;
            bool inString = false;

            StringBuilder propertyName = new StringBuilder();
            StringBuilder checkedValue = new StringBuilder();
            StringBuilder logicalName = new StringBuilder();
            ComparisonOperator comparison = ComparisonOperator.None;

            LogicalFilterNode lastLogicalNode = null;
            List<FilterNode> lastNodes = new List<FilterNode>();
            FilterNode root = null;

            for (int i = 0; i <= filter.Length; ++i)
            {
                char ch = ' ';

                if (i < filter.Length)
                {
                    // Letting i go to filter.Length makes it so that we do not have to handle any additional logic outside of the for loop
                    // We accomplish this by faking a space
                    ch = filter[i];
                }

                if (inString)
                {
                    if (ch == '\'')
                    {
                        inString = false;
                        continue;
                    }
                    checkedValue.Append(ch);
                }
                else if (inBracket)
                {
                    if (ch == ']')
                    {
                        inBracket = false;
                        lookingFor = LookingFor.ComparisonOperator;
                        continue;
                    }
                    propertyName.Append(ch);
                }
                else if (ch == ' ')
                {
                    switch (lookingFor)
                    {
                        case LookingFor.PropertyName:
                            if (string.Compare(propertyName.ToString(), "not", true) == 0)
                            {
                                LogicalFilterNode notLogicalNode = new LogicalFilterNode()
                                {
                                    LogicalOperator = LogicalOperator.Not
                                };
                                if (lastLogicalNode != null)
                                {
                                    lastLogicalNode.Right = notLogicalNode;
                                }
                                else if (root == null)
                                {
                                    root = notLogicalNode;
                                }
                                else
                                {
                                    throw new ArgumentException(string.Format("Looking for [AND|OR] at column {0} of '{1}'", i, filter));
                                }
                                lastNodes.Add(notLogicalNode);
                                propertyName.Length = 0;
                            }
                            else
                            {
                                var checkForNot = lastNodes.LastOrDefault() as LogicalFilterNode;
                                if (checkForNot != null && checkForNot.LogicalOperator == LogicalOperator.Not)
                                {
                                    checkForNot.Right = new ExpressionFilterNode()
                                    {
                                        ComparisonOperator = ComparisonOperator.None,
                                        Property = propertyName.ToString(),
                                    };
                                    lastNodes.Add(checkForNot.Right);
                                    lookingFor = LookingFor.LogicalOperator;
                                    propertyName.Length = 0;
                                }
                                else
                                {
                                    lookingFor = LookingFor.ComparisonOperator;
                                }
                            }
                            break;
                        case LookingFor.Value:
                            if (checkedValue.Length <= 0)
                            {
                                continue;
                            }
                            ExpressionFilterNode expressionNode = new ExpressionFilterNode() 
                                    { 
                                        ComparisonOperator = comparison,
                                        Property = propertyName.ToString(),
                                        Value = checkedValue.ToString()
                                    };
                            FilterNode lastNode = lastNodes.LastOrDefault();
                            if (lastNode != null)
                            {
                                if (lastNode is LogicalFilterNode)
                                {
                                    ((LogicalFilterNode)lastNode).Right = expressionNode;
                                }
                                else
                                {
                                    throw new ArgumentException(string.Format("Looking for logical operator but found {0} at column {1} of '{2}'", lastNode.GetType().Name, i, filter));
                                }
                            }
                            else
                            {
                                root = expressionNode;
                            }
                            lastNodes.Add(expressionNode);
                            propertyName.Length = 0;
                            checkedValue.Length = 0;
                            comparison = ComparisonOperator.None;
                            lookingFor = LookingFor.LogicalOperator;
                            break;
                        case LookingFor.LogicalOperator:
                            LogicalFilterNode currentLogicalNode = null;
                            switch (logicalName.ToString().ToLower())
                            {
                                case "and":
                                    lookingFor = LookingFor.PropertyName;
                                    currentLogicalNode = new LogicalFilterNode() 
                                            {
                                                Left = lastLogicalNode == null ? lastNodes.LastOrDefault() : lastLogicalNode,
                                                LogicalOperator = LogicalOperator.And 
                                            };
                                    lastLogicalNode = currentLogicalNode;
                                    lastNodes.Add(currentLogicalNode);
                                    root = currentLogicalNode;
                                    break;
                                case "or":
                                    lookingFor = LookingFor.PropertyName;
                                    currentLogicalNode = new LogicalFilterNode()
                                        {
                                            Left = lastLogicalNode == null ? lastNodes.LastOrDefault() : lastLogicalNode,
                                            LogicalOperator = LogicalOperator.Or
                                        };
                                    lastLogicalNode = currentLogicalNode;
                                    lastNodes.Add(currentLogicalNode);
                                    root = currentLogicalNode;
                                    break;
                                case "not":
                                    lookingFor = LookingFor.PropertyName;
                                    currentLogicalNode = new LogicalFilterNode() 
                                        {
                                            LogicalOperator = LogicalOperator.Not 
                                        };
                                    if (lastLogicalNode != null)
                                    {
                                        lastLogicalNode.Right = currentLogicalNode;
                                    }
                                    else if (root == null)
                                    {
                                        root = currentLogicalNode;
                                    }
                                    else
                                    {
                                        throw new ArgumentException(string.Format("Looking for [AND|OR] at column {0} of '{1}'", i, filter));
                                    }
                                    lastNodes.Add(currentLogicalNode);
                                    break;
                                default:
                                    throw new ArgumentException(string.Format("Looking for logical operator at column {0} of '{1}'", i, filter));
                                    break;
                            }
                            logicalName.Length = 0;
                            break;
                    }
                }
                else if (ch == '=' || ch == '<' || ch == '>')
                {
                    if (lookingFor != LookingFor.ComparisonOperator)
                    {
                        if (lastNodes.Count > 2 && lastNodes[lastNodes.Count - 2] is LogicalFilterNode && ((LogicalFilterNode)lastNodes[lastNodes.Count - 2]).LogicalOperator == LogicalOperator.Not)
                        {
                            LogicalFilterNode logicalNode = (LogicalFilterNode)lastNodes[lastNodes.Count - 2];

                            ExpressionFilterNode expressionNode = new ExpressionFilterNode()
                            {
                                ComparisonOperator = ComparisonOperator.None,
                                Property = propertyName.ToString(),
                            };

                            logicalNode.Right = expressionNode;
                            lookingFor = LookingFor.LogicalOperator;

                            lastNodes.Add(expressionNode);
                        }
                        else
                        {
                            throw new ArgumentException(string.Format("Looking for {0} at column {1} of '{2}'", lookingFor, i, filter));
                        }
                    }

                    comparison = ParseComparison(ch);
                    if (i + 1 < filter.Length && (filter[i + 1] == '=' || filter[i + 1] == '<' || filter[i + 1] == '>'))
                    {
                        comparison |= ParseComparison(filter[i + 1]);
                        ++i;
                    }

                    lookingFor = LookingFor.Value;
                }
                else if (ch == '\'')
                {
                    if (lookingFor != LookingFor.Value)
                    {
                        throw new ArgumentException(string.Format("Looking for {0} at column {1} of '{2}'", lookingFor, i, filter));
                    }
                    inString = true;
                }
                else if (ch == '[')
                {
                    if (lookingFor != LookingFor.PropertyName)
                    {
                        throw new ArgumentException(string.Format("Looking for {0} at column {1} of '{2}'", lookingFor, i, filter));
                    }
                    inBracket = true;
                }
                else
                {
                    switch (lookingFor)
                    {
                        case LookingFor.PropertyName:
                            propertyName.Append(ch);
                            break;
                        case LookingFor.LogicalOperator:
                            logicalName.Append(ch);
                            break;
                        case LookingFor.Value:
                            checkedValue.Append(ch);
                            break;
                        default:
                            throw new ArgumentException(string.Format("Looking for {0} at column {1} of '{2}'", lookingFor, i, filter));
                            break;
                    }

                }
            }

            return root;
        }

    }
}
