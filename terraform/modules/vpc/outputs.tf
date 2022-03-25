output "public_subnet_ids" {
  value = data.aws_subnet_ids.public.ids
}

output "private_subnet_ids" {
  value = data.aws_subnet_ids.private.ids
}

output "vpc_id" {
  value = aws_vpc.vpc.id
}

output "security_group_ids" {
  value = data.aws_security_groups.sg.ids
}