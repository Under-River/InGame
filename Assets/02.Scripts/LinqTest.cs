using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "허정범", Age = 28, Gender = "남" },
            new Student { Name = "박수현", Age = 26, Gender = "여" },
            new Student { Name = "박진혁", Age = 29, Gender = "남" },
            new Student { Name = "이상진", Age = 28, Gender = "남" },
            new Student { Name = "서민주", Age = 25, Gender = "여" },
            new Student { Name = "전태준", Age = 27, Gender = "남" },
            new Student { Name = "박순홍", Age = 27, Gender = "남" },
            new Student { Name = "양성일", Age = 29, Gender = "남" },
        };
        // '컬렉션'에서 '데이터'를 '조회(나열)'하는 일이 많습니다.
        // c#은 이런 비번한 작업을 편하게 하기 위해 LINQ 문법을
        // Language Integrated Query
        // 쿼리(Query): 질의 (데이터를 요청하거나 검색하는 명령문)

        // "From, In, Select, Where, ORDER BY" -> 데이터베이스 SELECT 문법과 비슷하다.
        // 사실 이렇게는 잘 쓰이지 않는다.
        IEnumerable<Student> all = from student in students select student;
        all = students.Where(student => true);

        foreach (var student in all)
        {
            Debug.Log("전체 : " + student);
        }

        Debug.Log("--------------------------------");
        
        IEnumerable<Student> boys = from student in students where student.Gender == "남" select student;
        boys = students.Where(student => student.Gender == "남");
        
        foreach (var student in boys)
        {
            Debug.Log("남자 : " + student);
        }

        Debug.Log("--------------------------------");

        IEnumerable<Student> girls = from student in students where student.Gender == "여" select student;
        girls = students.Where(student => student.Gender == "여");

        foreach (var student in girls)
        {
            Debug.Log("여자 : " + student);
        } 

        Debug.Log("--------------------------------");

        IEnumerable<Student> girs2 = from student in students
            where student.Gender == "여"
            orderby student.Age ascending
            select student;
        girls = students.Where(student => student.Gender == "여").OrderBy(student => student.Age);
        foreach (var student in girs2)
        {
            Debug.Log("여자 나이순 : " + student);
        }
        
        Debug.Log("--------------------------------");

        // GROUP BY, JOIN -> 데이터베이스 문법에서 사용되는

        // 장점: 편리하고, 가독성이 좋다.
        // 단점:
        // IEnumerable은 내부적으로 커서를 만드는데 이것이 나중에 쓰레기가 된다.
        // ㄴ 메모리가 증가한다.
        // ㄴ 쓰면 참 좋은데... 유니티 UPDATE에서 사용은 비추!

        // COUNT
        int mansCount = students.Count(student => student.Gender == "남");
        Debug.Log("남자 수 : " + mansCount);

        // SUM
        int totalAge = students.Sum(student => student.Age);
        Debug.Log("나이 합 : " + totalAge);
        int manstTotalAge = students.Where(student => student.Gender == "남").Sum(student => student.Age);
        Debug.Log("남자 나이 합 : " + manstTotalAge);

        // AVERAGE
        double averageAge = students.Average(student => student.Age);
        Debug.Log("나이 평균 : " + averageAge);
        double manstAverageAge = students.Where(student => student.Gender == "남").Average(student => student.Age);
        Debug.Log("남자 나이 평균 : " + manstAverageAge);

        // MAX
        int maxAge = students.Max(student => student.Age);
        Debug.Log("나이 최대 : " + maxAge);
        int manstMaxAge = students.Where(student => student.Gender == "남").Max(student => student.Age);
        Debug.Log("남자 나이 최대 : " + manstMaxAge);

        // 조건 만족? ALL(모두가 만족하니?) vs ANY(하나라도 만족하니?)
        // - 모두가 남자니?
        bool isAllMan = students.All(student => student.Gender == "남");
        Debug.Log("모두 남자? " + isAllMan);

        // - 30대 이상이 한명이라도 있니?
        bool isAnyOver30 = students.Any(student => student.Age >= 30);
        Debug.Log("30대 이상이 한명이라도 있니? " + isAnyOver30);
        
        // 정렬 문제
        // Sort 내장 함수는 내부적으로 마이크로 소프트가 이름 지어둔 인트로 소트를 쓴다.
        // 인트로 소트: 데이터의 크기, 종류등의 성질에 따라 Quick, Heap, Radix Sort를 짬뽕해서 적절히 쓰는 기법이다.
        students.Sort();
    }
}
