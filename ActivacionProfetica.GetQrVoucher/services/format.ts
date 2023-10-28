export const toDate = (dateString: string) : Date =>{
  const parts = dateString.split(/[- :]/);
  return new Date(parseInt(parts[2]), parseInt(parts[1])-1, parseInt(parts[0]), parseInt(parts[3]), parseInt(parts[4]), parseInt(parts[5]));
}
