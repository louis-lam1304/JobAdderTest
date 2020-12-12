export interface WeightedCandidate {
  candidateId: number;
  name: string;
  uniqueSkillTags: string[];
  jobWeight: number;
  foundSkills: string[];
  missingSkills: string[];
}
