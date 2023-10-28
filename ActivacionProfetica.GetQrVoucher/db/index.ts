import { PrismaClient } from '@prisma/client';

let orm: PrismaClient;

if (process.env.NODE_ENV === "production") {
  orm = new PrismaClient();
} else {
  let globalWithPrisma = global as typeof globalThis & {
    orm: PrismaClient;
  };
  if (!globalWithPrisma.orm) {
    globalWithPrisma.orm = new PrismaClient();
  }
  orm = globalWithPrisma.orm;
}

export default orm;