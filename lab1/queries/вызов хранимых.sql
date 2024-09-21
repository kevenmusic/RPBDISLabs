-- ������� ������ �������
EXEC InsertClient
    @FirstName = '����',
    @LastName = '������',
    @MiddleName = '��������',
    @Gender = '�������',
    @BirthDate = '1990-03-20',
    @ZodiacSignID = 5, -- ���
    @NationalityID = 6, -- ���������
    @Profession = '���������';

	-- ���������� ���������� ������������� �������
EXEC UpdatePhysicalAttributes
    @ClientID = 1,
    @Age = 39,
    @Height = 181.00,
    @Weight = 76.00,
    @ChildrenCount = 2,
    @MaritalStatus = '�����',
    @BadHabits = '�������',
    @Hobbies = '�����, ������';

	-- ������� ������ �������
EXEC UpsertAdditionalService
    @Name = '��������������� ������������',
    @Description = '������������ �� �������� ����������',
    @Price = 4500.00;

-- ���������� ������������� �������
EXEC UpsertAdditionalService
    @AdditionalServiceID = 1, -- ������ � ID = 1
    @Name = '��������� �������� �������',
    @Description = '���������� �������� �������',
    @Price = 5500.00;
