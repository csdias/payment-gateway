data "aws_subnet_ids" "public" {
  vpc_id = aws_vpc.vpc.id

  tags = {
    Tier = "Public"
  }
}

data "aws_subnet_ids" "private" {
  vpc_id = aws_vpc.vpc.id

  tags = {
    Tier = "Private"
  }
}

data "aws_security_groups" "sg" {
  filter {
    name   = "vpc-id"
    values = [aws_vpc.vpc.id]
  }
}