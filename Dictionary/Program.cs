using Dictionary;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Dictionary.Dictionary<int, string> dictionary = new Dictionary.Dictionary<int, string>();

            dictionary.Add(1, "일");
            dictionary.Add(2, "이");

            Console.WriteLine(dictionary.ContainsKey(2)); //true

            dictionary.Remove(2);

            Console.WriteLine(dictionary[1]); //반환
            dictionary[1] = "일산"; //변경
            dictionary[3] = "삼";
            dictionary.Clear();
            Console.WriteLine(dictionary.Count);
            //Console.WriteLine(dictionary[99]); //예외처리
        }
    }
}

namespace Dictionary
{
    public class Dictionary<TKey, TValue>
    {
        //키 값의 배열
        private TKey[] keys;
        //밸류 값의 배열
        private TValue[] values;
        //요소들 개수
        private int count;

        //생성자
        public Dictionary()
        {  
            keys = new TKey[10];
            values = new TValue[10];
            count = 0;
        }


        //key와 연결된 값을 반환, 변경, 생성, 없을 경우 예외를 발생하는 기능
        public TValue this[TKey key]
        {
            get
            {
                //keys라는 배열에서 key라는 요소를 찾는데, 0부터 count만큼(처음부터 끝까지) 찾아 인덱스를 반환
                int index = Array.IndexOf(keys, key, 0, count);
                
                //만약 그 인덱스가 없다면 -1를 반환한다
                if (index == -1)
                {
                    throw new KeyNotFoundException();
                }
                //인덱스가 있다면 그 인덱스의 값을 반환한다
                return values[index];
            }
            set
            {
                //keys라는 배열에서 key라는 요소를 찾는데, 0부터 count만큼(처음부터 끝까지) 찾아 인덱스를 반환
                int index = Array.IndexOf(keys, key, 0, count);

                //만약 그 인덱스가 없다면 -1를 반환한다
                if (index == -1)
                {
                    //요소 개수들이 keys배열의 크기만큼 차버렸다면
                    if (count == keys.Length)
                    {
                        //배열크기들을 두배로 조정한다
                        Array.Resize(ref keys, count * 2);
                        Array.Resize(ref values, count * 2);
                    }

                    //새로운 키-값 쌍 생성하는 기능

                    //keys 배열에다가 원하는 key값을 넣어준다
                    keys[count] = key;
                    //values 배열에다가 넣은 value값을 넣어준다
                    values[count] = value;
                    count++;
                }
                //그 인덱스가 있다면
                else
                {
                    //그 키와 연결된 값을 변경한다
                    values[index] = value;
                }
            }
        }

        //읽기 전용 Count
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// 요소를 추가해 주는 기능
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            //넣은 키의 인덱스에 넣은 밸류를 저장한다
            this[key] = value;
        }

        /// <summary>
        /// 키가 이미 딕셔너리에 존재하는지 bool값으로 반환하는 기능
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            //keys라는 배열에서 key라는 요소를 찾는데, 0부터 count만큼(처음부터 끝까지) 찾아 인덱스를 반환
            //만약 그 인덱스가 없다면 -1를 반환한다
            //찾게 된다면 -1이 아니기 때문에 true, 못 찾게 된다면 -1이기 때문에 false를 반환한다
            return Array.IndexOf(keys, key, 0, count) != -1;
        }

        /// <summary>
        /// 키가 있는지 찾고, 있다면 그 키와 연결된 값을 제거하는 기능  
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            //key라는 배열에서 key라는 요소를 찾는데, count만큼(처음부터 끝까지) 찾아 인덱스를 반환
            int index = Array.IndexOf(keys, key, 0, count);

            //그 인덱스가 없다면 -1를 반환하기 때문에 인덱스가 없다면
            if (index == -1)
            {
                //지우는 기능이 실행되지 않고 종료된다
                return false;
            }
            //인덱스를 찾았다면 인덱스부터 끝까지 반복
            for (int i = index; i < count - 1; i++)
            {
                //지우면 뒤에 인덱스가 한칸씩 앞으로 와야하기 때문에
                //한칸씩 당기는 반복
                keys[i] = keys[i + 1];
                values[i] = values[i + 1];
            }
            count--;
            //다 한칸씩 앞으로 당기고 맨 마지막 남은 요소를 
            //null 허용 값 형식이기에 기본값인 null을 넣어준다
            keys[count] = default(TKey);
            values[count] = default(TValue);
            return true;
        }

        /// <summary>
        /// 모든 요소들을 비워주는 기능
        /// </summary>
        public void Clear()
        {
            //keys(values)라는 배열에서 0부터 count만큼(처음부터 끝까지) 모든 값들을 다 기본값으로 변경(null)
            Array.Clear(keys, 0, count);
            Array.Clear(values, 0, count);
            count = 0;
        }
    }
}